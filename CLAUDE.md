# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Wintakam is a .NET MAUI cross-platform mobile application targeting Android, iOS, macOS Catalyst, and Windows. The project is built using .NET 9.0 and follows MVVM architecture patterns with dependency injection.

## Build & Development Commands

### Prerequisites
- .NET 9.0 SDK with MAUI workloads: `dotnet workload install maui`
- Verify workloads: `dotnet workload list`

### Common Commands

```bash
# Restore dependencies
dotnet restore

# Build for all platforms
dotnet build Wintakam.sln

# Build for specific platform
dotnet build -f net9.0-android
dotnet build -f net9.0-ios
dotnet build -f net9.0-maccatalyst
dotnet build -f net9.0-windows10.0.19041.0

# Run on Android
dotnet build -t:Run -f net9.0-android

# Run on Windows
dotnet build -t:Run -f net9.0-windows10.0.19041.0

# Clean build artifacts
dotnet clean
```

Note: The README.md mentions .NET 8.0, but the project has been upgraded to .NET 9.0 (see Wintakam.csproj:4-5).

## Architecture

### MVVM Pattern
The codebase follows a standard MAUI MVVM architecture with clear separation of concerns:

- **Views/** - XAML pages and UI definitions
- **ViewModels/** - Presentation logic using observable properties and commands
- **Models/** - Domain objects and data structures
- **Services/** - Data access, navigation, and platform-specific abstractions
- **Helpers/** - Utility classes and extension methods

ViewModels are intended to use `CommunityToolkit.Mvvm` attributes for cleaner, more testable code (mentioned in README but not yet implemented in the codebase).

### Dependency Injection
Services are registered in `MauiProgram.cs` (MauiProgram.cs:7-23). The application uses MAUI's built-in dependency injection container. New services should be registered in the `CreateMauiApp` method before `builder.Build()` is called.

### Application Entry Points
- **App.xaml.cs** - Application lifecycle (App.xaml.cs:3-14)
- **AppShell.xaml** - Shell navigation and routing configuration (AppShell.xaml:2-15)
- **MauiProgram.cs** - Dependency injection setup and MAUI configuration

The app uses `Shell.FlyoutBehavior="Flyout"` navigation (AppShell.xaml:7) with route-based navigation.

### Platform-Specific Code
Platform-specific implementations are located under `Platforms/`:
- `Platforms/Android/` - Android-specific code (MainActivity, MainApplication)
- `Platforms/iOS/` - iOS-specific code (AppDelegate, Program)
- `Platforms/MacCatalyst/` - macOS Catalyst-specific code
- `Platforms/Windows/` - Windows-specific code
- `Platforms/Tizen/` - Tizen support (optional, currently commented out in .csproj)

## Key Configuration Details

### Target Frameworks
The project targets multiple platforms (Wintakam.csproj:4-5):
- Android: `net9.0-android` (min SDK 21.0)
- iOS: `net9.0-ios` (min version 15.0)
- macOS Catalyst: `net9.0-maccatalyst` (min version 15.0)
- Windows: `net9.0-windows10.0.19041.0` (min version 10.0.17763.0)

### Project Settings
- **ApplicationId**: `com.companyname.wintakam` (Wintakam.csproj:27)
- **WindowsPackageType**: `None` - unpackaged deployment for development (Wintakam.csproj:34)
- **ImplicitUsings**: Enabled (Wintakam.csproj:20)
- **Nullable**: Enabled (Wintakam.csproj:21)

### Resources
Resources are configured in Wintakam.csproj:44-60:
- App icon with foreground: `Resources/AppIcon/`
- Splash screen: `Resources/Splash/splash.svg`
- Images: `Resources/Images/`
- Fonts: `Resources/Fonts/` (includes FluentUI font)
- Styles: `Resources/Styles/` (Colors.xaml, Styles.xaml)
- Raw assets: `Resources/Raw/`

## Development Workflow

### Adding New Features
1. Create domain models in `Models/`
2. Create services in `Services/` and register them in `MauiProgram.cs`
3. Create ViewModels in `ViewModels/` with observable properties
4. Create XAML views in `Views/` bound to ViewModels
5. Register routes in `AppShell.xaml` for navigation

### Testing
The README mentions unit testing with `dotnet test`, but no test project currently exists. ViewModel-first unit testing is recommended before wiring to UI.

## Notes

- This is an academic project for the EPD-GIT-5541 course at Polytech Douala
- The folder structure (Views/, ViewModels/, Models/, Services/, Helpers/) is established but currently contains only .gitkeep files
- Debug logging is enabled in DEBUG builds via `Microsoft.Extensions.Logging.Debug` (Wintakam.csproj:64)
