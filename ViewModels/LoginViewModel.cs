using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Wintakam.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private string _email = string.Empty;
    private string _password = string.Empty;
    private bool _rememberMe;
    private bool _isPasswordVisible;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Email
    {
        get => _email;
        set
        {
            if (_email != value)
            {
                _email = value;
                OnPropertyChanged();
            }
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            if (_password != value)
            {
                _password = value;
                OnPropertyChanged();
            }
        }
    }

    public bool RememberMe
    {
        get => _rememberMe;
        set
        {
            if (_rememberMe != value)
            {
                _rememberMe = value;
                OnPropertyChanged();
            }
        }
    }

    public bool IsPasswordVisible
    {
        get => _isPasswordVisible;
        set
        {
            if (_isPasswordVisible != value)
            {
                _isPasswordVisible = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand LoginCommand { get; }
    public ICommand TogglePasswordVisibilityCommand { get; }
    public ICommand GoogleLoginCommand { get; }
    public ICommand FacebookLoginCommand { get; }
    public ICommand RegisterCommand { get; }
    public ICommand BackCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = new Command(OnLogin);
        TogglePasswordVisibilityCommand = new Command(OnTogglePasswordVisibility);
        GoogleLoginCommand = new Command(OnGoogleLogin);
        FacebookLoginCommand = new Command(OnFacebookLogin);
        RegisterCommand = new Command(OnRegister);
        BackCommand = new Command(OnBack);
    }

    private async void OnLogin()
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            await Shell.Current.DisplayAlert(
                "Erreur",
                "Veuillez saisir votre email et mot de passe",
                "OK");
            return;
        }

        // TODO: Implement actual login logic
        await Shell.Current.DisplayAlert(
            "Connexion",
            $"Email: {Email}\nSe souvenir: {RememberMe}",
            "OK");
    }

    private void OnTogglePasswordVisibility()
    {
        IsPasswordVisible = !IsPasswordVisible;
    }

    private async void OnGoogleLogin()
    {
        // TODO: Implement Google OAuth login
        await Shell.Current.DisplayAlert(
            "Google Login",
            "Connexion avec Google à implémenter",
            "OK");
    }

    private async void OnFacebookLogin()
    {
        // TODO: Implement Facebook OAuth login
        await Shell.Current.DisplayAlert(
            "Facebook Login",
            "Connexion avec Facebook à implémenter",
            "OK");
    }

    private async void OnRegister()
    {
        // TODO: Navigate to registration page
        await Shell.Current.GoToAsync("//register");
    }

    private async void OnBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
