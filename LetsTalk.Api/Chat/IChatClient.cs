using LetsTalk.Shared.DTO;

namespace LetsTalk.Api.Chat;

public interface IChatClient
{
    Task ReceiveMessage(OpenMessage message);
}