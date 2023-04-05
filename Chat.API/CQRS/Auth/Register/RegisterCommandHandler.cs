using Chat.API.CQRS.Base;
using Chat.API.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Chat.API.CQRS.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request,
            CancellationToken cancellationToken)
        {
            // logic

            var result = await _userManager.CreateAsync(new()
            {
                UserName = request.Username,
                Email = request.Email,
            }, request.Password);


            if (result.Succeeded)
                return new RegisterCommandResponse()
                {
                    Status = Result.Success,
                    Message = "Başarıyla kayıt oldunuz.",
                    Data = null
                };

            return new RegisterCommandResponse()
            {
                Status = Result.Error,
                Message = "Kayıt olurken hata oluştu.",
                Data = result.Errors.Select(x=> x.Description).ToList()
            };
        }
    }
}