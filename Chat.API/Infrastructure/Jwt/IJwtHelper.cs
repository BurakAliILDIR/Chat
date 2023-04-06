using Chat.API.Entities;

namespace Chat.API.Infrastructure.Jwt
{
    public interface IJwtHelper
    {
        string CreateToken(AppUser user);
    }
}
