using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp;
using System.Xml.Linq;

namespace Chat.API.Hubs
{
    public class MyHub : Hub
    {
        public static List<string> Names { get; set; }

        public async Task SendName(string name)
        {
            await Clients.All.SendAsync("ReceiveName", name);
        }

        public async Task GetNames()
        {
            await Clients.All.SendAsync("ReceiveNames", Names);
        }
    }
}