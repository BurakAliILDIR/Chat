namespace Chat.API.CQRS.Meet.GetMessage.Dto
{
    public class GetMessageDto
    {
        public string ReceiverId { get; init; }
        public string SenderId { get; init; }
        public string Text { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
