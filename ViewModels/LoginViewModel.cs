using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Wintakam.Models;
using Wintakam.Services;

namespace Wintakam.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private readonly IAuthenticationService _authService;
    private string _email = string.Empty;
    private string _password = string.Empty;
    private bool _rememberMe;
    private bool _isPasswordVisible;
    private bool _isBusy;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Email
    {
        get => _email;
        set { if (_email != value) { _email = value; OnPropertyChanged(); } }
    }

    public string Password
    {
        get => _password;
        set { if (_password != value) { _password = value; OnPropertyChanged(); } }
    }

    public bool RememberMe
    {
        get => _rememberMe;
        set { if (_rememberMe != value) { _rememberMe = value; OnPropertyChanged(); } }
    }

    public bool IsPasswordVisible
    {
        get => _isPasswordVisible;
        set { if (_isPasswordVisible != value) { _isPasswordVisible = value; OnPropertyChanged(); } }
    }

    public bool IsBusy
    {
        get => _isBusy;
        set { if (_isBusy != value) { _isBusy = value; OnPropertyChanged(); } }
    }

    public ICommand LoginCommand { get; }
    public ICommand TogglePasswordVisibilityCommand { get; }
    public ICommand GoogleLoginCommand { get; }
    public ICommand FacebookLoginCommand { get; }
    public ICommand RegisterCommand { get; }
    public ICommand BackCommand { get; }

    public LoginViewModel(IAuthenticationService authService)
    {
        _authService = authService;

        LoginCommand = new Command(async () => await OnLoginAsync(), () => !IsBusy);
        TogglePasswordVisibilityCommand = new Command(OnTogglePasswordVisibility);
        GoogleLoginCommand = new Command(OnGoogleLogin);
        FacebookLoginCommand = new Command(OnFacebookLogin);
        RegisterCommand = new Command(async () => await OnRegisterAsync());
        BackCommand = new Command(async () => await OnBackAsync());
    }

    private async Task OnLoginAsync()
    {
        if (IsBusy) return;

        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            await Shell.Current.DisplayAlert("Erreur", "Veuillez saisir votre email et mot de passe", "OK");
            return;
        }

        if (!IsValidEmail(Email))
        {
            await Shell.Current.DisplayAlert("Erreur", "Veuillez saisir une adresse email valide", "OK");
            return;
        }

        IsBusy = true;

        try
        {
            var loginRequest = new LoginRequest
            {
                Email = Email.Trim(),
                Password = Password,
                RememberMe = RememberMe
            };

            var result = await _authService.SignInAsync(loginRequest);

            if (result.Success)
            {
                Password = string.Empty;
                // Navigate to main app with tabs
                await Shell.Current.GoToAsync("//MainTabs/PropertyListPage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Échec de connexion", result.ErrorMessage ?? "Une erreur s'est produite", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Erreur", $"Une erreur inattendue s'est produite: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void OnTogglePasswordVisibility()
    {
        IsPasswordVisible = !IsPasswordVisible;
    }

    private async void OnGoogleLogin()
    {
        await Shell.Current.DisplayAlert("Non disponible", "La connexion via Google n'est pas encore disponible.", "OK");
    }

    private async void OnFacebookLogin()
    {
        await Shell.Current.DisplayAlert("Non disponible", "La connexion via Facebook n'est pas encore disponible.", "OK");
    }

    private async Task OnRegisterAsync()
    {
        await Shell.Current.DisplayAlert("Bientôt disponible", "La page d'inscription sera bientôt disponible.", "OK");
    }

    private async Task OnBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
