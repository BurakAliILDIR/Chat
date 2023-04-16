using Chat.API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat.API
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}