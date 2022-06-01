using LetsTalk.Shared.Constants;

namespace LetsTalk.Shared.DTO;

// ReSharper disable once ClassNeverInstantiated.Global
public record OpenMessage(string Message, UserResponseDto Sender, MessagePrivacy Privacy);
