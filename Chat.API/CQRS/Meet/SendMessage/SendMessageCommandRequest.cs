using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Chat.API.CQRS.Meet.SendMessage
{
    public class SendMessageCommandRequest : IRequest<SendMessageCommandResponse>
    {
        public string ReceiverId { get; set; }
        public string Text { get; set; }
    }
}