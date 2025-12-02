# Wintakam

Wintakam is a cross-platform real estate management mobile application built with .NET MAUI. The application features user authentication, property listing management, and a modern UI following MAUI design patterns. This project is developed as part of the EPD-GIT-5541 Atelier de Developpement Mobile course at Polytech Douala.

## Features

- **User Authentication** - Secure login and registration powered by Supabase Auth
- **Property Management** - Browse and manage real estate listings
- **Cross-Platform** - Runs on Android, iOS, macOS, and Windows
- **Modern UI** - Clean interface with custom theming
- **Offline Support** - Mock services for development without backend

## Prerequisites

- .NET 9.0 SDK with the MAUI workloads installed (`dotnet workload install maui`)
- Visual Studio 2022 (17.8+) or VS Code with the C# Dev Kit for debugging
- Android/iOS emulators or a physical device configured for deployment
- A Supabase account (free tier works fine) for authentication features

## Quick Start

> **⚠️ SECURITY NOTICE**: For evaluation purposes, Supabase credentials are temporarily included below. These will be removed and rotated after course evaluation for security reasons.

### 1. Configuration

**For Quick Testing (Instructors & Evaluators):**

The project is pre-configured with a test Supabase instance. You can skip the configuration step and go directly to step 2.

Current configuration (`appsettings.json`):
```json
{
  "Supabase": {
    "Url": "https://vawdvlzgpclnmmvtxzkn.supabase.co",
    "AnonKey": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZhd2R2bHpncGNsbm1tdnR4emtuIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NjQ2NzQ3NDIsImV4cCI6MjA4MDI1MDc0Mn0.RwmVjqy5o_jsaurBQQ6UlalraOU3tQK-RHrbYCtLzWU"
  }
}
```

**For Development with Your Own Backend:**

Copy the example configuration file and add your own Supabase credentials:

```bash
# Windows
copy appsettings.example.json appsettings.json

# macOS/Linux
cp appsettings.example.json appsettings.json
```

Then edit `appsettings.json` with your Supabase project URL and anon key. See [SETUP.md](SETUP.md) for detailed setup instructions including database configuration.

### 2. Build and Run

```bash
# Restore NuGet packages
dotnet restore

# Build the project
dotnet build Wintakam.sln

# Run on Windows
dotnet build -t:Run -f net9.0-windows10.0.19041.0

# Run on Android
dotnet build -t:Run -f net9.0-android

# Run on iOS
dotnet build -t:Run -f net9.0-ios

# Run on macOS
dotnet build -t:Run -f net9.0-maccatalyst
```

### 3. Test Credentials

For testing the application, you can use the following default test account:

- **Email:** `test@example.com`
- **Password:** `Test123!`

This account is pre-configured in the Supabase backend for development and testing purposes.

## Project Structure

```
Wintakam/
├── Views/                    # XAML pages and UI definitions
│   ├── LoginPage.xaml       # User login interface
│   ├── WelcomePage.xaml     # Initial welcome screen
│   ├── HomePage.xaml        # Main dashboard
│   └── PropertyListPage.xaml # Property browsing
├── ViewModels/              # Presentation logic with observable properties
│   ├── LoginViewModel.cs    # Login page logic
│   ├── HomeViewModel.cs     # Home page logic
│   └── PropertyListViewModel.cs # Property list logic
├── Models/                  # Domain objects and data structures
│   ├── User.cs             # User entity
│   ├── AuthResult.cs       # Authentication response
│   ├── Property.cs         # Property entity
│   ├── PropertyType.cs     # Property classification
│   └── PropertyStatus.cs   # Property availability status
├── Services/               # Business logic and data access
│   ├── IAuthenticationService.cs       # Auth contract
│   ├── SupabaseAuthenticationService.cs # Supabase auth implementation
│   ├── IPropertyService.cs             # Property data contract
│   ├── SupabasePropertyService.cs      # Supabase property implementation
│   ├── MockPropertyService.cs          # Offline development service
│   └── ConfigurationService.cs         # App configuration loader
├── Helpers/                # Utility classes
│   └── IsNotNullConverter.cs # XAML value converter
├── Resources/              # Assets and styling
│   ├── Styles/            # XAML styles and themes
│   ├── Images/            # Application images
│   └── Fonts/             # Custom fonts
└── Platforms/             # Platform-specific code
    ├── Android/
    ├── iOS/
    ├── MacCatalyst/
    └── Windows/
```

## Architecture

The application follows the **MVVM (Model-View-ViewModel)** pattern with dependency injection:

- **Views** are XAML pages that define the UI
- **ViewModels** handle presentation logic and data binding
- **Models** represent domain entities
- **Services** provide data access and business logic abstraction

Key architectural decisions:
- **Dependency Injection**: Services are registered in `MauiProgram.cs` and injected into ViewModels
- **Shell Navigation**: Route-based navigation defined in `AppShell.xaml`
- **Service Abstraction**: Interfaces allow switching between real (Supabase) and mock implementations
- **Configuration Management**: Settings loaded from `appsettings.json` via `ConfigurationService`

## Technologies & Dependencies

- **.NET 9.0** with MAUI framework
- **Supabase C#** (`supabase-csharp` v0.16.2) - Backend authentication and data storage
- **System.Text.Json** - JSON configuration parsing
- **Microsoft.Extensions.Logging.Debug** - Debug logging

## Development Notes

1. **Adding New Services**: Register services in `MauiProgram.cs` to make them available via dependency injection
2. **MVVM Pattern**: Use observable properties and commands in ViewModels for data binding
3. **Navigation**: Register new routes in `AppShell.xaml` and use Shell navigation methods
4. **Configuration**: Store sensitive data in `appsettings.json` (never commit this file)
5. **Platform Code**: Use platform-specific folders under `Platforms/` for native customizations
6. **Mock Services**: Use mock implementations during development to work offline

## Documentation

- **[SETUP.md](SETUP.md)** - Complete setup guide with Supabase configuration and database setup
- **[CLAUDE.md](CLAUDE.md)** - Instructions for Claude Code when working with this codebase
- **[AGENTS.md](AGENTS.md)** - AI agent guidance for development tasks

## Testing & Quality

Currently, the project does not have a dedicated test project. To add testing:

```bash
# Create a test project
dotnet new nunit -n Wintakam.Tests
dotnet sln add Wintakam.Tests/Wintakam.Tests.csproj
```

**Testing Guidelines:**
- Unit test ViewModels independently before wiring to UI
- Test service implementations with mock data
- Validate navigation flows and user workflows
- Run `dotnet workload list` periodically to ensure MAUI workloads are installed

## Troubleshooting

### Configuration Issues
- Ensure `appsettings.json` exists and contains valid Supabase credentials
- Verify the file is in the project root and properly formatted
- Rebuild: `dotnet clean && dotnet build`

### Build Errors
- Confirm .NET 9.0 SDK is installed: `dotnet --version`
- Ensure MAUI workloads are installed: `dotnet workload list`
- Try restoring workloads: `dotnet workload restore`

### Authentication Issues
- Check Supabase project is active (not paused)
- Verify credentials in Supabase Dashboard
- Ensure database tables and policies are created (see SETUP.md)

See [SETUP.md](SETUP.md) for more detailed troubleshooting steps.

## Contributing

This is an academic project for EPD-GIT-5541 at Polytech Douala. To contribute:

1. Create a local branch for your feature: `git checkout -b feature/your-feature-name`
2. Make your changes following the MVVM pattern
3. Test your changes on at least one platform
4. Commit with clear, descriptive messages
5. Submit a pull request with:
   - Description of the changes
   - Screenshots or recordings for UI updates
   - Reference to course requirements if applicable

## License

This project is intended for academic use within the Polytech Douala program. Please obtain permission before reusing the code outside the course context.
