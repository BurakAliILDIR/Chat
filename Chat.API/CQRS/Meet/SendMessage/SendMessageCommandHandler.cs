using System.Security.Claims;
using Chat.API.Configs;
using Chat.API.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Chat.API.CQRS.Meet.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommandRequest, SendMessageCommandResponse>
    {
        private readonly IMongoCollection<Entities.Message> _meetCollection;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MongoDbSettings _mongoDbSettings;

        public SendMessageCommandHandler(IHttpContextAccessor httpContextAccessor, IOptions<MongoDbSettings> mongoDbSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _mongoDbSettings = mongoDbSettings.Value;

            IMongoClient mongoClient = new MongoClient(_mongoDbSettings.ConnectionStrings);
            IMongoDatabase db = mongoClient.GetDatabase(_mongoDbSettings.DatabaseName);
            _meetCollection = db.GetCollection<Entities.Message>(_mongoDbSettings.MeetCollectionName);
        }

        public async Task<SendMessageCommandResponse> Handle(SendMessageCommandRequest request,
            CancellationToken cancellationToken)
        {
            var senderId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var message = new Message(receiverId: request.ReceiverId, senderId: senderId, text: request.Text);

            await _meetCollection.InsertOneAsync(message);

            return new SendMessageCommandResponse();
        }
    }
}