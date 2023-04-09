using Chat.API.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.API.Controllers
{
    public class NotificationController : BaseController
    {
        private readonly IHubContext<MyHub> _hubContext;

        public NotificationController(IHubContext<MyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet("{teamCount}")]
        public async Task<IActionResult> Index(uint teamCount)
        {
            MyHub.TeamCount = teamCount;

            await _hubContext.Clients.All.SendAsync("Notify", $"Arkadaşlar takımlar {teamCount} kişiden olacaktır.");

            return Ok();
        }
    }
}
