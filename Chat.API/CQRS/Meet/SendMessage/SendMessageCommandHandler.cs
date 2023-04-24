using System.Security.Claims;
using Chat.API.Configs;
using Chat.API.Entities;
using Chat.API.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Chat.API.CQRS.Meet.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommandRequest, SendMessageCommandResponse>
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHubContext<MessageHub> _hubContext;

        public SendMessageCommandHandler(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor,
            IHubContext<MessageHub> hubContext)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _hubContext = hubContext;
        }

        public async Task<SendMessageCommandResponse> Handle(SendMessageCommandRequest request,
            CancellationToken cancellationToken)
        {
            var senderId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);


            var meet = await _dbContext.Meets.Where(x =>
                    (x.SenderId == senderId || x.ReceiverId == senderId) &&
                    (x.SenderId == request.ReceiverId || x.ReceiverId == request.ReceiverId))
                .FirstOrDefaultAsync();

            bool isMeet = meet is null;

            if (isMeet) meet = new();

            meet.LastMessage = request.Text;
            meet.CreatedAt = DateTime.UtcNow;

            if (isMeet)
            {
                meet.Id = Guid.NewGuid();
                meet.SenderId = senderId;
                meet.ReceiverId = request.ReceiverId;
                await _dbContext.Meets.AddAsync(meet);
            }

            var message = new Message(meetId: meet.Id, receiverId: request.ReceiverId, senderId: senderId,
                text: request.Text);

            await _dbContext.Messages.AddAsync(message);

            _hubContext.Clients.Users(request.ReceiverId, senderId)
                .SendAsync("ReceiveMessage", new
                {
                    id = message.Id,
                    senderId = message.SenderId,
                    receiverId = message.ReceiverId,
                    meetId = message.MeetId,
                    text = message.Text,
                }, cancellationToken);

            await _dbContext.SaveChangesAsync();


            return new SendMessageCommandResponse();
        }
    }
}