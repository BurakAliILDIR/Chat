using System.Security.Claims;
using Chat.API.Entities;
using MediatR;
using MongoDB.Driver;

namespace Chat.API.CQRS.Meet.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommandRequest, SendMessageCommandResponse>
    {
        private readonly IMongoCollection<Entities.Message> _meetCollection;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SendMessageCommandHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            IMongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = mongoClient.GetDatabase("ChatDb");
            _meetCollection = db.GetCollection<Entities.Message>("Meets");
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