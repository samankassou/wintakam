# Wintakam

Wintakam is a .NET MAUI mobile application developed as part of the EPD-GIT-5541 Atelier de Developpement Mobile course.

## Prerequisites

- .NET 8.0 SDK with the MAUI workloads installed (`dotnet workload install maui`)
- Visual Studio 2022 (17.8+) or VS Code with the C# Dev Kit for debugging
- Android/iOS emulators or a physical device configured for deployment

## Getting Started

```bash
# Restore NuGet packages
dotnet restore

# Build the project
dotnet build Wintakam.sln

# Run on Android (example target)
dotnet build -t:Run -f net8.0-android
```

## Project Structure

- `Views/` - XAML pages and UI definitions
- `ViewModels/` - Presentation logic using observable properties and commands
- `Models/` - Domain objects shared across the app
- `Services/` - Abstractions for data access, navigation, and platform helpers
- `Resources/` - Styles, images, fonts, and localization assets

## Development Notes

1. Register new services inside `MauiProgram.cs` to make them available via dependency injection.
2. Leverage `CommunityToolkit.Mvvm` attributes to keep view-models concise and testable.
3. Use platform-specific folders under `Platforms/` for native customizations when needed.

## Testing & Quality

- Unit tests can be added with `dotnet test` once a test project is created.
- Run `dotnet workload list` periodically to ensure required workloads remain installed.
- Prefer ViewModel-first unit tests before wiring new pages to the UI.

## Contributing

Fork the repository (or create a local branch), make your changes, and submit a pull request describing the feature or fix. Please include screenshots or screen recordings for UI updates and reference any related requirements from the course brief.

## License

This project is intended for academic use within the Polytech Douala program. Please obtain permission before reusing the code outside the course context.
