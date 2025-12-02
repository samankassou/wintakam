namespace Wintakam
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes for navigation
            Routing.RegisterRoute(nameof(Views.LoginPage), typeof(Views.LoginPage));
            Routing.RegisterRoute(nameof(Views.HomePage), typeof(Views.HomePage));
            Routing.RegisterRoute(nameof(Views.PropertyListPage), typeof(Views.PropertyListPage));
        }
    }
}
