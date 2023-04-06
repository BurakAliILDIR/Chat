using Chat.API.CQRS.Base;

namespace Chat.API.CQRS.Auth.Login
{
    public class LoginQueryResponse : BaseResponse
    {
        public string AccessToken { get; set; }

    }
}
