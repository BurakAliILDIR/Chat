using MediatR;

namespace Chat.API.CQRS.User.GetAll
{
    public class GetAllUserQueryRequest : IRequest<GetAllUserQueryResponse>
    {
    }
}