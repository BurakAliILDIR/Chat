using MediatR;

namespace Chat.API.CQRS.Auth.Login
{
    public class LoginQueryRequest : IRequest<LoginQueryResponse>
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
