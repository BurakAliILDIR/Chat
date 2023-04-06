using MediatR;

namespace Chat.API.CQRS.Meet.GetMessage
{
    public class GetMessageQueryRequest : IRequest<GetMessageQueryResponse>
    {
        public string ReceiveId { get; set; }

    }
}
