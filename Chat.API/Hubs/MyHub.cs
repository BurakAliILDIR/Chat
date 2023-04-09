using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp;
using System.Xml.Linq;

namespace Chat.API.Hubs
{
    public class MyHub : Hub
    {
        // eğer static prop olmazsa her nesne oluşturulduğunda değer sıfırlanır.
        private static uint ClientCount { get; set; }

        private static List<string> Names { get; set; } = new();
        public static uint TeamCount { get; set; } = 7;

        public async Task SendName(string name)
        {
            if (Names.Count >= TeamCount)
            {
                // Dönüş yapılırken sadece iletiyi gönderen kişinin görmesini sağlar.
                await Clients.Caller.SendAsync("Error", $"Takım en fazla {TeamCount} kişi olabilir.");
            }
            else
            {
                Names.Add(name);
                await Clients.All.SendAsync("ReceiveName", name);
            }
        }

        public async Task GetNames()
        {
            await Clients.All.SendAsync("ReceiveNames", Names);
        }

        // Her client bağlandığında çalışır.
        public override async Task OnConnectedAsync()
        {
            ClientCount++;
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
            await base.OnConnectedAsync();
        }


        // Her client disconnect olduğunda çalışır.
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            ClientCount--;
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
            await base.OnDisconnectedAsync(exception);
        }

        // Groups
        public async Task AddToGroup(string teamName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, teamName);
        }

        public async Task RemoveToGroup(string teamName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, teamName);
        }

        public async Task SendNameByGroup(string name, string teamName)
        {
            await Clients.Group(teamName).SendAsync("ReceiveMessageByGroup", name);
        }

    }
}