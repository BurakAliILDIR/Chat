namespace Chat.API.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid MeetId { get; init; }
        public string? ReceiverId { get; init; }
        public string SenderId { get; init; }
        public string Text { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? DeletedAt { get; set; }

        public Message()
        {
        }

        public Message(Guid meetId, string? receiverId, string senderId, string text)
        {
            Id = Guid.NewGuid();
            MeetId = meetId;
            ReceiverId = receiverId;
            SenderId = senderId;
            Text = text;
            CreatedAt = DateTime.UtcNow;
        }
    }
}