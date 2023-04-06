using MediatR;

namespace Chat.API.CQRS.Auth.ForgotPassword
{
    public class ForgotPasswordQueryRequest: IRequest<ForgotPasswordQueryResponse>
    {
        public required string UsernameOrEmail { get; set; }
    }
}
