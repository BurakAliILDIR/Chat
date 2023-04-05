using Chat.API.CQRS.Base;
using MediatR;

namespace Chat.API.CQRS.Auth.Register
{
    public class RegisterCommandRequest : IRequest<RegisterCommandResponse>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}