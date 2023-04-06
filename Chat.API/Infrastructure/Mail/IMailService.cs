using Chat.API.Entities;

namespace Chat.API.Infrastructure.Mail
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailData mailData, CancellationToken ct);
    }
}
