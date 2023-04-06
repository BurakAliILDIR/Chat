using Chat.API.CQRS.Meet.GetMessage;
using Chat.API.CQRS.Meet.SendMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers.Meet
{
    [Authorize]
    public class MeetsController : BaseController
    {
        private readonly IMediator _mediator;

        public MeetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessageCommandRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("GetMessages")]
        public async Task<IActionResult> GetMessages(GetMessageQueryRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}