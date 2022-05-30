using Microsoft.AspNetCore.SignalR;

namespace LetsTalk.Api.Chat;

public class ChatHub : Hub<IChatClient>
{
    public async Task SendMessageToAll(string userIdentifier, string message)
    {
        await Clients.All.ReceiveMessage(userIdentifier, message);
    }

    public async Task SendPrivateMessage(string userIdentifier, string message)
    {
        await Clients.User(userIdentifier).SendPrivateMessage(userIdentifier, message);
    }
}