using Chat.API.Configs;

namespace Chat.API.Extensions
{
    public static class ConfigExtension
    {
        public static void AddCustomConfigs(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MailSettings>(config.GetSection(nameof(MailSettings)));
            services.Configure<RedirectorSettings>(config.GetSection(nameof(RedirectorSettings)));
            services.Configure<JwtSettings>(config.GetSection(nameof(JwtSettings)));
            services.Configure<MongoDbSettings>(config.GetSection(nameof(MongoDbSettings)));
        }
    }
}