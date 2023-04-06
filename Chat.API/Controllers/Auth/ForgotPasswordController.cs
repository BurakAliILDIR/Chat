using Chat.API.CQRS.Auth.ForgotPassword;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers.Auth
{
    public class ForgotPasswordController : BaseController
    {
        private readonly IMediator _mediator;

        public ForgotPasswordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordQueryRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}