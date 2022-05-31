using LetsTalk.Shared.DTO;
using Microsoft.AspNetCore.SignalR;

namespace LetsTalk.Api.Chat;

public class ChatHub : Hub<IChatClient>
{
    public async Task SendMessageToAll(OpenMessage openMessage)
    {
        await Clients.All.ReceiveMessage(openMessage);
    }

    public async Task SendPrivateMessage(PrivateMessage privateMessage)
    {
        var openMessage = new OpenMessage(privateMessage.Message, privateMessage.Sender);

        await Clients.User(privateMessage.ReceiverId).ReceiveMessage(openMessage);
    }
}