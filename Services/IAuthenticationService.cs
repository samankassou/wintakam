using Wintakam.Models;

namespace Wintakam.Services;

public interface IAuthenticationService
{
    Task<AuthResult> SignInAsync(LoginRequest request);
    Task SignOutAsync();
    Task<User?> GetCurrentUserAsync();
    Task<bool> IsAuthenticatedAsync();
    Task<bool> RestoreSessionAsync();
}
