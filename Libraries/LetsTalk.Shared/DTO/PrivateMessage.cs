namespace LetsTalk.Shared.DTO;

// ReSharper disable once ClassNeverInstantiated.Global
public record PrivateMessage(UserResponseDto Sender, string ReceiverId, string Message);