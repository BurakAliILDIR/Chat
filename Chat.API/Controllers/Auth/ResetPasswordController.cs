using Chat.API.CQRS.Auth.ResetPassword;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers.Auth
{
    public class ResetPasswordController : BaseController
    {
        private readonly IMediator _mediator;

        public ResetPasswordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommandRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}