namespace Wintakam.Models;

public class AuthResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public User? User { get; set; }

    public static AuthResult Succeed(User user) => new()
    {
        Success = true,
        User = user
    };

    public static AuthResult Fail(string errorMessage) => new()
    {
        Success = false,
        ErrorMessage = errorMessage
    };
}
