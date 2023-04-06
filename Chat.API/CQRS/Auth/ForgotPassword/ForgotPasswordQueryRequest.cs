using MediatR;

namespace Chat.API.CQRS.Auth.ForgotPassword
{
    public class ForgotPasswordQueryRequest: IRequest<ForgotPasswordQueryResponse>
    {
        public string UsernameOrEmail { get; set; }
    }
}
