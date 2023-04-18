using Chat.API.Entities;
using MediatR;
using MongoDB.Driver;
using System.Security.Claims;
using AutoMapper;
using Chat.API.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Chat.API.CQRS.Meet.GetMessage
{
    public class GetMessageQueryHandler : IRequestHandler<GetMessageQueryRequest, GetMessageQueryResponse>
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetMessageQueryHandler(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<GetMessageQueryResponse> Handle(GetMessageQueryRequest request,
            CancellationToken cancellationToken)
        {
            var senderId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var meetId = Message.GetId(request.ReceiveId, senderId);

            var meet = await _dbContext.Meets.Include(x => x.Messages.OrderByDescending(x => x.CreatedAt))
                .Where(x => x.Id == meetId).FirstOrDefaultAsync();

            var meetDto = _mapper.Map<List<GetMeetDto>>(meet);

            return new GetMessageQueryResponse()
            {
                Data = meetDto
            };
        }
    }
}