using MediatR;

namespace Chat.API.CQRS.Auth.EmailConfirmation
{
    public class EmailConfirmationCommandRequest : IRequest<EmailConfirmationCommandResponse>
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}