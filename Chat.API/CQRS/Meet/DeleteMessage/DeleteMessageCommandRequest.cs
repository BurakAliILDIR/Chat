using MediatR;

namespace Chat.API.CQRS.Meet.DeleteMessage
{
    public class DeleteMessageCommandRequest : IRequest<DeleteMessageCommandResponse>
    {
        public string Id { get; set; }
    }
}