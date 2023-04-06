using Chat.API.Entities;
using MediatR;
using MongoDB.Driver;
using System.Security.Claims;
using AutoMapper;
using Chat.API.CQRS.Meet.GetMessage.Dto;

namespace Chat.API.CQRS.Meet.GetMessage
{
    public class GetMessageQueryHandler : IRequestHandler<GetMessageQueryRequest, GetMessageQueryResponse>
    {
        private readonly IMongoCollection<Entities.Message> _meetCollection;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetMessageQueryHandler(IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;

            IMongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = mongoClient.GetDatabase("ChatDb");
            _meetCollection = db.GetCollection<Entities.Message>("Meets");
        }

        public async Task<GetMessageQueryResponse> Handle(GetMessageQueryRequest request,
            CancellationToken cancellationToken)
        {
            var senderId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<Message> messages = await _meetCollection
                .FindAsync(x => x.SenderId == senderId && x.ReceiverId == request.ReceiveId).Result
                .ToListAsync();


            var messageDtos = _mapper.Map<List<MessageDto>>(messages);


            return new GetMessageQueryResponse()
            {
                Messages = messageDtos
            };
        }
    }
}