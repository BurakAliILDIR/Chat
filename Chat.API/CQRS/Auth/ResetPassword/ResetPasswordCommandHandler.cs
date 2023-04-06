using Chat.API.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Chat.API.CQRS.Auth.ResetPassword
{
    public class
        ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommandRequest, ResetPasswordCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public ResetPasswordCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResetPasswordCommandResponse> Handle(ResetPasswordCommandRequest request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

           var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

           if (!result.Succeeded)
               throw new Exception("Parola değiştirirken hata oluştu.");

           return new ResetPasswordCommandResponse();
        }
    }
}