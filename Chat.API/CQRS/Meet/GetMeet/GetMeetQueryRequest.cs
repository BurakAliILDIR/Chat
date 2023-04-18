using MediatR;

namespace Chat.API.CQRS.Meet.GetMeet
{
    public class GetMeetQueryRequest : IRequest<GetMeetQueryResponse>
    {
    }
}