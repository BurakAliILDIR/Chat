using Chat.API.CQRS.Auth.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers.Auth
{
    public class RefreshTokenController : BaseController
    {
        private readonly IMediator _mediator;

        public RefreshTokenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken(RefreshTokenQueryRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}