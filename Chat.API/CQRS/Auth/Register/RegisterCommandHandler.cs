using System.Security.Policy;
using System.Text;
using Chat.API.Configs;
using Chat.API.CQRS.Base;
using Chat.API.Entities;
using Chat.API.Infrastructure.Mail;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Chat.API.CQRS.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        private readonly RedirectorSettings _redirectorSettings;

        public RegisterCommandHandler(UserManager<AppUser> userManager,
            IMailService mailService, IConfiguration configuration, IOptions<RedirectorSettings> redirectorSettings)
        {
            _userManager = userManager;
            _mailService = mailService;
            _configuration = configuration;
            _redirectorSettings = redirectorSettings.Value;
        }

        public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(new()
            {
                UserName = request.Username,
                Email = request.Email,
            }, request.Password);


            if (!result.Succeeded)
                return new RegisterCommandResponse()
                {
                    Status = Result.Error,
                    Message = "Kayıt olurken hata oluştu.",
                    Data = result.Errors.Select(x => x.Description).ToList()
                };

            var user = await _userManager.FindByNameAsync(request.Username);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var callbackUrl = _configuration.GetSection("RedirectorSettings:EmailConfirmationPage").Value +
                              $"?userId={user.Id}&token={token}";

            await _mailService.SendAsync(
                new MailData(
                    to: new() { user.Email }, 
                    subject: "Email Onaylama",
                    body: @$"<h4>Hesabınınızı aktif etmek için aşağıdaki linke gidiniz.</h4>
                                <p>
                                    <a href='{callbackUrl}'>Hesap Onaylama Linki</a>
                                </p>"),
                cancellationToken);

            return new RegisterCommandResponse()
            {
                Status = Result.Success,
                Message = "Başarıyla kayıt oldunuz.",
                Data = null
            };
        }
    }
}