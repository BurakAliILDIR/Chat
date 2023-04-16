using MediatR;

namespace Chat.API.CQRS.Auth.RefreshToken
{
    public class RefreshTokenQueryRequest: IRequest<RefreshTokenQueryResponse>
    {
        public string RefreshToken { get; set; }
    }
}
