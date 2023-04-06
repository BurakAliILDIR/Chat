using Chat.API.Entities;
using Chat.API.Exceptions.Auth;
using Chat.API.Infrastructure.Mail;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Chat.API.CQRS.Auth.ForgotPassword
{
    public class ForgotPasswordQueryHandler : IRequestHandler<ForgotPasswordQueryRequest, ForgotPasswordQueryResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMailService _mailService;

        public ForgotPasswordQueryHandler(UserManager<AppUser> userManager, IMailService mailService)
        {
            _userManager = userManager;
            _mailService = mailService;
        }

        public async Task<ForgotPasswordQueryResponse> Handle(ForgotPasswordQueryRequest request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UsernameOrEmail);

            if (user is null)
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (user is null)
                throw new NotFoundUserException("Sent email.");

            await _mailService.SendAsync(new MailData(to: new List<string> { user.Email }, subject: "Test"),
                cancellationToken);

            return new ForgotPasswordQueryResponse();
        }
    }
}