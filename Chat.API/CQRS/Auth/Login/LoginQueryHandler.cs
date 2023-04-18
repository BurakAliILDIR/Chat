using Chat.API.Configs;
using Chat.API.Entities;
using Chat.API.Infrastructure.Jwt;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Chat.API.CQRS.Auth.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQueryRequest, LoginQueryResponse>
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtHelper _jwtHelper;
        private readonly JwtSettings _jwtSettings;

        public LoginQueryHandler(AppDbContext dbContext, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IJwtHelper jwtHelper, IOptions<JwtSettings> jwtSettings)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtHelper = jwtHelper;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<LoginQueryResponse> Handle(LoginQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UsernameOrEmail);

            if (user is null)
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (user is null)
                throw new Exception("Giriş bilgileriniz yanlış.");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, true);

            if (!result.Succeeded)
                throw new Exception("Giriş bilgileriniz yanlış.");

            var token = _jwtHelper.CreateToken(user);

            var refreshTokenId = Guid.NewGuid();

            await _dbContext.RefreshTokens.AddAsync(new Entities.RefreshToken
            {
                Id = refreshTokenId,
                UserId = user.Id,
                ExpireAt = DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshTokenMinute),
                CreatedAt = DateTime.UtcNow
            });

            await _dbContext.SaveChangesAsync();

            return new()
            {
                Data = new Dictionary<string, object>()
                {
                    { "accessToken", token },
                    { "refreshToken", refreshTokenId }
                }
            };
        }
    }
}