using AutoMapper;
using Chat.API.CQRS.Meet.GetMessage;
using MediatR;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Chat.API.Entities;
using System.Collections.Generic;

namespace Chat.API.CQRS.Meet.GetMeet
{
    public class GetMeetQueryHandler : IRequestHandler<GetMeetQueryRequest, GetMeetQueryResponse>
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetMeetQueryHandler(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<GetMeetQueryResponse> Handle(GetMeetQueryRequest request, CancellationToken cancellationToken)
        {
            var senderId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var meets = await _dbContext.Meets.Where(x => x.SenderId == senderId || x.ReceiverId == senderId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();


            var meetsDto = new List<GetMeetDto>();

            foreach (var meet in meets)
            {
                meetsDto.Add(new GetMeetDto()
                {
                    Id = meet.Id,
                    LastMessage = meet.LastMessage,
                    CreatedAt = meet.CreatedAt,
                    Receiver = meet.SenderId == senderId ? meet.ReceiverId : senderId
                });
            }

            return new GetMeetQueryResponse()
            {
                Data = meetsDto
            };
        }
    }
}