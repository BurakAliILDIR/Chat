using Chat.API.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Chat.API.CQRS.Meet.DeleteMessage
{
    public class
        DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommandRequest, DeleteMessageCommandResponse>
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHubContext<MessageHub> _hubContext;

        public DeleteMessageCommandHandler(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor,
            IHubContext<MessageHub> hubContext)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _hubContext = hubContext;
        }

        public async Task<DeleteMessageCommandResponse> Handle(DeleteMessageCommandRequest request,
            CancellationToken cancellationToken)
        {
            var senderId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);


            var message = await _dbContext.Messages.Where(x =>
                    (x.SenderId == senderId || x.ReceiverId == senderId) && x.Id == Guid.Parse(request.Id))
                .FirstOrDefaultAsync();

            message.DeletedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            _hubContext.Clients.Users(message.SenderId, message.ReceiverId).SendAsync("DeletedMessage", request.Id);

            return new DeleteMessageCommandResponse();
        }
    }
}