using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Chat.API.Entities
{
    public class Meet
    {
        public Guid Id { get; set; }
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public string LastMessage { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}