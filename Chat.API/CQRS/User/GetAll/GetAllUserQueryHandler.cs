using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AutoMapper;
using Chat.API.Entities;

namespace Chat.API.CQRS.User.GetAll
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQueryRequest, GetAllUserQueryResponse>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllUserQueryHandler(AppDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetAllUserQueryResponse> Handle(GetAllUserQueryRequest request,
            CancellationToken cancellationToken)
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _dbContext.Users.Where(x => userName != x.UserName).ToListAsync();

            var data = _mapper.Map<List<GetAllUserDto>>(result);

            return new GetAllUserQueryResponse() { Data = data };
        }
    }
}