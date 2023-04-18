using Chat.API.Entities;

namespace Chat.API.CQRS.Meet.GetMessage
{
    public class GetMeetDto
    {
        public Guid Id { get; init; }
        public string? Receiver { get; set; }
        public string LastMessage { get; init; }
        public DateTime CreatedAt { get; init; }

        public ICollection<Message> Messages { get; init; }
    }
}