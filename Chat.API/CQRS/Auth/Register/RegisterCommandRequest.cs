using Chat.API.CQRS.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Chat.API.CQRS.Auth.Register
{
    public class RegisterCommandRequest : IRequest<RegisterCommandResponse>
    {
        public required string Username { get; set; }

        [EmailAddress] public required string Email { get; set; }

        public required string Password { get; set; }

        [Compare("Password", ErrorMessage = "Parola ve parola onay eşleşmiyor.")]
        public required string PasswordConfirm { get; set; }
    }
}