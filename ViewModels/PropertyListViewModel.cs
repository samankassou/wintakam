using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Wintakam.Models;
using Wintakam.Services;

namespace Wintakam.ViewModels;

public class PropertyListViewModel : INotifyPropertyChanged
{
    private readonly IPropertyService _propertyService;
    private bool _isLoading;
    private bool _isRefreshing;
    private string _errorMessage = string.Empty;
    private bool _hasError;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<Property> Properties { get; } = new();

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            if (_isLoading != value)
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }
    }

    public bool IsRefreshing
    {
        get => _isRefreshing;
        set
        {
            if (_isRefreshing != value)
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            if (_errorMessage != value)
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
    }

    public bool HasError
    {
        get => _hasError;
        set
        {
            if (_hasError != value)
            {
                _hasError = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasNoProperties));
            }
        }
    }

    public bool HasNoProperties => !HasError && !IsLoading && Properties.Count == 0;

    public ICommand LoadPropertiesCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand PropertyTappedCommand { get; }

    public PropertyListViewModel(IPropertyService propertyService)
    {
        _propertyService = propertyService;

        LoadPropertiesCommand = new Command(async () => await LoadPropertiesAsync());
        RefreshCommand = new Command(async () => await RefreshPropertiesAsync());
        PropertyTappedCommand = new Command<Property>(async (property) => await OnPropertyTappedAsync(property));

        System.Diagnostics.Debug.WriteLine("PropertyListViewModel: Constructor - Starting initialization");
        System.Diagnostics.Debug.WriteLine($"PropertyListViewModel: IsLoading={IsLoading}, HasError={HasError}, Properties.Count={Properties.Count}, HasNoProperties={HasNoProperties}");

        // Fire and forget initialization
        _ = LoadPropertiesAsync();
    }

    private async Task LoadPropertiesAsync()
    {
        System.Diagnostics.Debug.WriteLine("PropertyListViewModel: LoadPropertiesAsync - Starting");

        if (IsLoading)
        {
            System.Diagnostics.Debug.WriteLine("PropertyListViewModel: LoadPropertiesAsync - Already loading, returning");
            return;
        }

        try
        {
            IsLoading = true;
            System.Diagnostics.Debug.WriteLine($"PropertyListViewModel: IsLoading set to true. HasNoProperties={HasNoProperties}");
            HasError = false;
            ErrorMessage = string.Empty;

            System.Diagnostics.Debug.WriteLine("PropertyListViewModel: Calling GetAllPropertiesAsync...");
            var properties = await _propertyService.GetAllPropertiesAsync();
            System.Diagnostics.Debug.WriteLine($"PropertyListViewModel: Received {properties.Count} properties from service");

            Properties.Clear();
            foreach (var property in properties)
            {
                Properties.Add(property);
                System.Diagnostics.Debug.WriteLine($"PropertyListViewModel: Added property: {property.Title}");
            }

            OnPropertyChanged(nameof(HasNoProperties));
            System.Diagnostics.Debug.WriteLine($"PropertyListViewModel: Loading complete. Properties.Count={Properties.Count}, HasNoProperties={HasNoProperties}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"PropertyListViewModel: Error loading properties: {ex.GetType().Name} - {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"PropertyListViewModel: Stack trace: {ex.StackTrace}");
            HasError = true;
            ErrorMessage = TranslateError(ex.Message);
            System.Diagnostics.Debug.WriteLine($"PropertyListViewModel: HasError={HasError}, ErrorMessage={ErrorMessage}");
        }
        finally
        {
            IsLoading = false;
            System.Diagnostics.Debug.WriteLine($"PropertyListViewModel: IsLoading set to false. HasNoProperties={HasNoProperties}");
        }
    }

    private async Task RefreshPropertiesAsync()
    {
        if (IsRefreshing)
            return;

        try
        {
            IsRefreshing = true;
            HasError = false;
            ErrorMessage = string.Empty;

            var properties = await _propertyService.GetAllPropertiesAsync();

            Properties.Clear();
            foreach (var property in properties)
            {
                Properties.Add(property);
            }

            OnPropertyChanged(nameof(HasNoProperties));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error refreshing properties: {ex.Message}");
            HasError = true;
            ErrorMessage = TranslateError(ex.Message);
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    private async Task OnPropertyTappedAsync(Property? property)
    {
        if (property == null)
            return;

        try
        {
            await Shell.Current.DisplayAlert(
                "Bientôt disponible",
                $"La page de détails pour '{property.Title}' sera bientôt disponible.",
                "OK"
            );
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error handling property tap: {ex.Message}");
        }
    }

    private string TranslateError(string message)
    {
        return message.ToLower() switch
        {
            var msg when msg.Contains("network") => "Erreur de connexion. Vérifiez votre internet.",
            var msg when msg.Contains("timeout") => "Le serveur ne répond pas. Réessayez plus tard.",
            var msg when msg.Contains("unauthorized") => "Session expirée. Veuillez vous reconnecter.",
            _ => "Une erreur s'est produite. Veuillez réessayer."
        };
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
