namespace LetsTalk.Api.Chat;

public interface IChatClient
{
    Task ReceiveMessage(string user, string message);
    Task SendPrivateMessage(string user, string message);
}