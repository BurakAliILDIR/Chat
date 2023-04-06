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

        [HttpGet]
        public IActionResult ForgotPassword(ForgotPasswordQueryRequest request)
        {
            var response = _mediator.Send(request);

            return Ok(response);
        }
    }
}