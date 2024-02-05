using MedicalAppointments.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace MedicalAppointments.Models
{
    public sealed class ChatSignalR : Hub<IChatSignalR>
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId} has joined");
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId}: {message}");
        }
    }
}
