using Supabase;
using Supabase.Gotrue;
using Wintakam.Models;
using Session = Supabase.Gotrue.Session;
using SupabaseUser = Supabase.Gotrue.User;

namespace Wintakam.Services;

public class SupabaseAuthenticationService : IAuthenticationService
{
    private readonly Supabase.Client _supabaseClient;
    private const string SessionKey = "supabase_session";

    public SupabaseAuthenticationService(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public async Task<AuthResult> SignInAsync(LoginRequest request)
    {
        try
        {
            var session = await _supabaseClient.Auth.SignIn(request.Email, request.Password);

            if (session?.User == null)
                return AuthResult.Fail("Échec de la connexion. Vérifiez vos identifiants.");

            if (request.RememberMe)
                await StoreSessionAsync(session);

            var user = MapToUser(session.User);
            return AuthResult.Succeed(user);
        }
        catch (Supabase.Gotrue.Exceptions.GotrueException ex)
        {
            return AuthResult.Fail(TranslateError(ex.Message));
        }
        catch (Exception ex)
        {
            return AuthResult.Fail($"Erreur de connexion: {ex.Message}");
        }
    }

    public async Task SignOutAsync()
    {
        try
        {
            await _supabaseClient.Auth.SignOut();
            await ClearSessionAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Logout error: {ex.Message}");
        }
    }

    public async Task<Models.User?> GetCurrentUserAsync()
    {
        try
        {
            var user = _supabaseClient.Auth.CurrentUser;
            return user != null ? MapToUser(user) : null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var user = await GetCurrentUserAsync();
        return user != null;
    }

    public async Task<bool> RestoreSessionAsync()
    {
        try
        {
            var sessionJson = await SecureStorage.GetAsync(SessionKey);
            if (string.IsNullOrEmpty(sessionJson))
                return false;

            var isAuthenticated = await IsAuthenticatedAsync();
            if (!isAuthenticated)
                await ClearSessionAsync();

            return isAuthenticated;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Session restore error: {ex.Message}");
            await ClearSessionAsync();
            return false;
        }
    }

    private async Task StoreSessionAsync(Session session)
    {
        try
        {
            var sessionJson = System.Text.Json.JsonSerializer.Serialize(new
            {
                AccessToken = session.AccessToken,
                RefreshToken = session.RefreshToken
            });
            await SecureStorage.SetAsync(SessionKey, sessionJson);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Session store error: {ex.Message}");
        }
    }

    private async Task ClearSessionAsync()
    {
        try
        {
            SecureStorage.Remove(SessionKey);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Session clear error: {ex.Message}");
        }
    }

    private Models.User MapToUser(SupabaseUser supabaseUser)
    {
        return new Models.User
        {
            Id = supabaseUser.Id,
            Email = supabaseUser.Email ?? string.Empty,
            CreatedAt = supabaseUser.CreatedAt,
            LastSignInAt = supabaseUser.LastSignInAt
        };
    }

    private string TranslateError(string englishMessage)
    {
        return englishMessage.ToLower() switch
        {
            var msg when msg.Contains("invalid login credentials") => "Email ou mot de passe incorrect.",
            var msg when msg.Contains("email not confirmed") => "Veuillez confirmer votre email.",
            var msg when msg.Contains("invalid email") => "Adresse email invalide.",
            _ => "Une erreur s'est produite. Veuillez réessayer."
        };
    }
}
