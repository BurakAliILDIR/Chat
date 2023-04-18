namespace Chat.API.CQRS.Meet.GetMessage
{
    public class GetMessageDto
    {
        public string ReceiverId { get; init; }
        public string SenderId { get; init; }
        public string Text { get; init; }
        public long CreatedAt { get; init; }
    }
}