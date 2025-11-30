using System.Windows.Input;

namespace Wintakam.ViewModels;

public class WelcomeViewModel
{
    public ICommand ContinueCommand { get; }

    public WelcomeViewModel()
    {
        ContinueCommand = new Command(OnContinue);
    }

    private async void OnContinue()
    {
        // Navigate to the login page after welcome
        await Shell.Current.GoToAsync("//LoginPage");
    }
}
