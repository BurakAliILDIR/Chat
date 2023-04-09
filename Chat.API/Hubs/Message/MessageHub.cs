using System.Security.Claims;
using Chat.API.CQRS.Meet.SendMessage;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat.API.Hubs.Message
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessageHub : Hub
    {
        private readonly IMediator _mediator;

        public MessageHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendMessage(SendMessageCommandRequest request)
        {
            await _mediator.Send(request);

            var kendisi = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await Clients.Users(kendisi).SendAsync("ReceiveMessage", kendisi + ": " + request.Text);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("GetConnectionId", Context.ConnectionId);
        }

        #region Group

        public async Task AddGroup(string connectionId, string groupName)
        {
            await Groups.AddToGroupAsync(connectionId, groupName);
        }

        public async Task SendGroupMessage(string message, string groupName)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
        }

        #endregion
    }
}

#region Clients.Caller

// Sadece server'a bildirim gönderen Client ile iletişim kurar.

#endregion

#region All

// Server'a bağlı olan tüm client'larla iletişim kurar.

#endregion

#region Other

// Sadece server'a bildirim gönderen client dışında, Server'a bağlı olan tüm client'lara mesaj gönderir.

#endregion

#region Hub Client Methodları

#region AllExcept

// Belirtilen client'lar hariç server'a bağlı olan tüm clientlara bildiride bulunur.

#endregion

#region Client

// Server'a bağlı olan client'lar arasında sadece connection ID'si verilen tek client'a mesaj gönderir.

#endregion

#region Clients

// Server'a bağlı olan client'lar arasında sadece connection ID'si verilen client'lara mesaj gönderir.

#endregion

#region Group

// Belirtilen gruptaki tüm client'lara bildiride bulunur.
// Önce gruplar oluşturulmalı ve ardından client'lar gruplara subscribe olmalı.

#endregion

#region GroupExcept

// Belirtilen gruptaki tüm client'lar dışındaki tüm client'lara mesaj iletmemizi sağlayan bir fonksiyondur.

#endregion

#region Groups

// Birden çok groupdaki client'a bildiri yapabilmemizi sağlayan fonksiyondur.

#endregion

#region OthersInGroup

// Bildiride bulunan client haricinde groupdaki tüm client'lara bildiride bulunan fonksiyondur.

#endregion

#region User

#endregion

#endregion