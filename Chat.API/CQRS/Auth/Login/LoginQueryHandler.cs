using Chat.API.Entities;
using Chat.API.Exceptions.Auth;
using Chat.API.Infrastructure.Jwt;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Chat.API.CQRS.Auth.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQueryRequest, LoginQueryResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtHelper _jwtHelper;


        public LoginQueryHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IJwtHelper jwtHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtHelper = jwtHelper;
        }

        public async Task<LoginQueryResponse> Handle(LoginQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UsernameOrEmail);

            if (user is null)
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (user is null)
                throw new NotFoundUserException("Your information is wrong.");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, true);

            if (!result.Succeeded)
                throw new NotFoundUserException("Your information is wrong.");

            var token = _jwtHelper.CreateToken(user);

            return new()
            {
                Data = token
            };
        }
    }
}