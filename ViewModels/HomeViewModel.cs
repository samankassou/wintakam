using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Wintakam.Services;

namespace Wintakam.ViewModels;

public class HomeViewModel : INotifyPropertyChanged
{
    private readonly IAuthenticationService _authService;
    private string _welcomeMessage = "Chargement...";
    private string _userEmail = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string WelcomeMessage
    {
        get => _welcomeMessage;
        set { if (_welcomeMessage != value) { _welcomeMessage = value; OnPropertyChanged(); } }
    }

    public string UserEmail
    {
        get => _userEmail;
        set { if (_userEmail != value) { _userEmail = value; OnPropertyChanged(); } }
    }

    public ICommand LogoutCommand { get; }
    public ICommand NavigateToPropertiesCommand { get; }

    public HomeViewModel(IAuthenticationService authService)
    {
        _authService = authService;
        LogoutCommand = new Command(async () => await OnLogoutAsync());
        NavigateToPropertiesCommand = new Command(async () => await OnNavigateToPropertiesAsync());
        _ = LoadUserInfoAsync();
    }

    private async Task LoadUserInfoAsync()
    {
        try
        {
            var user = await _authService.GetCurrentUserAsync();

            if (user != null)
            {
                WelcomeMessage = "Bienvenue!";
                UserEmail = user.Email;
            }
            else
            {
                WelcomeMessage = "Bienvenue!";
                UserEmail = "Utilisateur";
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading user: {ex.Message}");
            WelcomeMessage = "Bienvenue!";
        }
    }

    private async Task OnLogoutAsync()
    {
        var confirm = await Shell.Current.DisplayAlert("Déconnexion", "Êtes-vous sûr de vouloir vous déconnecter?", "Oui", "Non");
        if (!confirm) return;

        try
        {
            await _authService.SignOutAsync();
            // Return to the welcome page
            await Shell.Current.GoToAsync($"//{nameof(Views.WelcomePage)}");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Erreur", $"Erreur lors de la déconnexion: {ex.Message}", "OK");
        }
    }

    private async Task OnNavigateToPropertiesAsync()
    {
        try
        {
            await Shell.Current.GoToAsync(nameof(Views.PropertyListPage));
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Erreur", $"Erreur de navigation: {ex.Message}", "OK");
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
