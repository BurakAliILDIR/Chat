using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Chat.API.CQRS.User.GetAll
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQueryRequest, GetAllUserQueryResponse>
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllUserQueryHandler(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetAllUserQueryResponse> Handle(GetAllUserQueryRequest request,
            CancellationToken cancellationToken)
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _dbContext.Users.Where(x => userName != x.UserName).ToListAsync();

            return new GetAllUserQueryResponse() { Data = result };
        }
    }
}