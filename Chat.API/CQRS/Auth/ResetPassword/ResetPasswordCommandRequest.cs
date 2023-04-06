using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Chat.API.CQRS.Auth.ResetPassword
{
    public class ResetPasswordCommandRequest : IRequest<ResetPasswordCommandResponse>
    {
        public required string UserId { get; set; }
        public required string Token { get; set; }
        public required string Password { get; set; }

        [Compare("Password", ErrorMessage = "Parola ve parola onay eşleşmiyor.")]
        public required string PasswordConfirm { get; set; }
    }
}