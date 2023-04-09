namespace Chat.API.Hubs.Message
{
    public interface IMessageHub
    {
        public Task SendMessage();
    }
}
