using MediatR;

namespace Chat.API.CQRS.Auth.Login
{
    public class LoginQueryRequest : IRequest<LoginQueryResponse>
    {
        public required string UsernameOrEmail { get; set; }
        public required string Password { get; set; }
    }
}
