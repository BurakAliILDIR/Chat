using Chat.API.Entities;
using MediatR;
using MongoDB.Driver;
using System.Security.Claims;
using AutoMapper;
using Chat.API.Configs;
using Microsoft.Extensions.Options;

namespace Chat.API.CQRS.Meet.GetMessage
{
    public class GetMessageQueryHandler : IRequestHandler<GetMessageQueryRequest, GetMessageQueryResponse>
    {
        private readonly IMongoCollection<Entities.Message> _meetCollection;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly MongoDbSettings _mongoDbSettings;

        public GetMessageQueryHandler(IHttpContextAccessor httpContextAccessor, IMapper mapper,
            IOptions<MongoDbSettings> mongoDbSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _mongoDbSettings = mongoDbSettings.Value;

            IMongoClient mongoClient = new MongoClient(_mongoDbSettings.ConnectionStrings);
            IMongoDatabase db = mongoClient.GetDatabase(_mongoDbSettings.DatabaseName);
            _meetCollection = db.GetCollection<Entities.Message>(_mongoDbSettings.MeetCollectionName);
        }

        public async Task<GetMessageQueryResponse> Handle(GetMessageQueryRequest request,
            CancellationToken cancellationToken)
        {
            var senderId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var meetId = Message.GetId(request.ReceiveId, senderId);

            List<Message> messages = await _meetCollection
                .FindAsync(x => x.MeetId == meetId).Result
                .ToListAsync();


            var messageDtos = _mapper.Map<List<GetMessageDto>>(messages);

            return new GetMessageQueryResponse()
            {
                Data = messageDtos
            };
        }
    }
}