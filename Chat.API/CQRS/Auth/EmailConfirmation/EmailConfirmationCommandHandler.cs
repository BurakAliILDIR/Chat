using Chat.API.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Chat.API.CQRS.Auth.EmailConfirmation
{
    public class
        EmailConfirmationCommandHandler : IRequestHandler<EmailConfirmationCommandRequest,
            EmailConfirmationCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public EmailConfirmationCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<EmailConfirmationCommandResponse> Handle(EmailConfirmationCommandRequest request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
                throw new Exception("Kullanıcı bulunamadı.");

            var decodeToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

            var result = await _userManager.ConfirmEmailAsync(user, decodeToken);

            if (!result.Succeeded)
                throw new Exception("Email onaylanamadı.");

            return new EmailConfirmationCommandResponse();
        }
    }
}