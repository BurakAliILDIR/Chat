namespace Chat.API.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public string MeetId { get; init; }
        public string ReceiverId { get; init; }
        public string SenderId { get; init; }
        public string Text { get; init; }
        public DateTime CreatedAt { get; init; }


        public Message(string meetId, string receiverId, string senderId, string text)
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