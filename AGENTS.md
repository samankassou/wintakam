# Repository Guidelines

## Project Structure & Modules
- Core entry points are App.xaml, AppShell.xaml, and MauiProgram.cs in the repository root.
- UI layout lives in the Views folder (XAML pages) with code-behind in matching .xaml.cs files.
- Page state and navigation logic live in ViewModels (MVVM pattern).
- Domain models are under Models; cross-cutting helpers are under Helpers; backend integration code is under Services.
- Static assets such as icons, fonts, styles, and images are in the Resources tree. Do not edit bin or obj.

## Build, Run, and Test
- dotnet restore – restore all NuGet dependencies.
- dotnet build – compile the MAUI project for all configured targets.
- dotnet build -t:Run – run the app for the default target platform.
- Use your IDE (Visual Studio, Rider) MAUI run configurations for emulator or device-specific runs.
- When a test project exists, run dotnet test from the solution root before pushing.

## Coding Style and Naming
- Use standard C# style: 4-space indentation, spaces not tabs, braces on new lines.
- Name classes, interfaces, and public members using PascalCase (for example PropertyListViewModel, IPropertyService).
- Name local variables and private fields using camelCase; private fields may be prefixed with an underscore when consistent with nearby code.
- Keep XAML page names descriptive (for example LoginPage, PropertyListPage) and match each view with a corresponding view model.
- Run your editor or IDE formatter for C# and XAML before committing.

## Testing Guidelines
- Place future tests in a sibling test project such as Wintakam.Tests using xUnit or NUnit as agreed by the team.
- Name test classes after the unit under test, for example LoginViewModelTests.
- Focus coverage on view models and services; exercise UI indirectly via these layers.
- Ensure all tests pass with dotnet test before opening a pull request once tests are present.

## Commits and Pull Requests
- Write clear commit messages with an imperative subject line, for example Add property list view, and an optional explanatory body.
- Keep changes focused so each branch or pull request addresses one feature or fix where practical.
- In pull request descriptions include a short summary, testing notes such as dotnet build and dotnet test, and screenshots for notable UI changes.
- Link any related issues or tasks using their identifiers when applicable.
