namespace Wintakam
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes for navigation
            Routing.RegisterRoute(nameof(Views.LoginPage), typeof(Views.LoginPage));
        }
    }
}
