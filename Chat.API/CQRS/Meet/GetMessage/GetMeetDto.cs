using Chat.API.Entities;

namespace Chat.API.CQRS.Meet.GetMessage
{
    public class GetMeetDto
    {
        public string Id { get; set; }
        public string LastMessage { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}