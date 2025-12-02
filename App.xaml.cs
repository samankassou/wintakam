using Wintakam.Services;

namespace Wintakam
{
    public partial class App : Application
    {
        private readonly IAuthenticationService _authService;

        public App(IAuthenticationService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await RestoreSessionAsync();
        }

        private async Task RestoreSessionAsync()
        {
            try
            {
                var sessionRestored = await _authService.RestoreSessionAsync();

                if (sessionRestored)
                {
                    // Navigate to HomePage
                    await Shell.Current.GoToAsync($"//{nameof(Views.WelcomePage)}/{nameof(Views.HomePage)}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Session restore error: {ex.Message}");
            }
        }
    }
}
