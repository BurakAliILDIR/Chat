using Chat.API.CQRS.Base;
using MediatR;

namespace Chat.API.CQRS.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
    {
        public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            // logic

            return new RegisterCommandResponse()
            {
                Status = Result.Success,
                Message = "Başarıyla kayıt oldunuz.",
                Data = null
            };
        }
    }
}