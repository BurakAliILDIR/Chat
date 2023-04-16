
namespace Chat.API.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public long ExpireAt { get; set; }
        public long CreatedAt { get; set; }

    }
}