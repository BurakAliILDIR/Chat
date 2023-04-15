using System.Text;
using System.Web;
using Chat.API.Configs;
using Chat.API.Entities;
using Chat.API.Infrastructure.Mail;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Chat.API.CQRS.Auth.ForgotPassword
{
    public class ForgotPasswordQueryHandler : IRequestHandler<ForgotPasswordQueryRequest, ForgotPasswordQueryResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMailService _mailService;
        private readonly RedirectorSettings _redirectorSettings;

        public ForgotPasswordQueryHandler(UserManager<AppUser> userManager, IMailService mailService,
            IOptions<RedirectorSettings> redirectorSettings)
        {
            _userManager = userManager;
            _mailService = mailService;
            _redirectorSettings = redirectorSettings.Value;
        }

        public async Task<ForgotPasswordQueryResponse> Handle(ForgotPasswordQueryRequest request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UsernameOrEmail);

            if (user is null)
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (user is null)
                throw new Exception("Sent email.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var callbackUrl = _redirectorSettings.ResetPasswordPage + $"?userId={user.Id}&token={token}";

            var result = await _mailService.SendAsync(new MailData(
                to: new List<string> { user.Email },
                subject: "Parolamı Unuttum",
                body: @$"<h4>Şifrenizi değiştirmek için aşağıdaki linke gidiniz.</h4>
                                <p>
                                    <a href='{callbackUrl}'>Şifre Değiştirme Linki</a>
                                </p>"), cancellationToken);

            if (!result)
            {
                throw new Exception("Parola sıfırlama maili gönderilemedi.");
            }

            return new ForgotPasswordQueryResponse();
        }
    }
}