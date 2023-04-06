using Chat.API.Entities;
using Chat.API.Exceptions.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

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
                throw new NotFoundUserException("Kullanıcı bulunamadı.");

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);

            if (!result.Succeeded)
                throw new Exception("Email onaylanamadı.");

            return new EmailConfirmationCommandResponse();
        }
    }
}