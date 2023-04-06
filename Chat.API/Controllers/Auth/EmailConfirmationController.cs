using Chat.API.CQRS.Auth.EmailConfirmation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers.Auth
{
    public class EmailConfirmationController : BaseController
    {
        private readonly IMediator _mediator;

        public EmailConfirmationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> EmailConfirmation(EmailConfirmationCommandRequest request)
        {
           var response =  await _mediator.Send(request);

           return Ok(response);
        }
    }
}
