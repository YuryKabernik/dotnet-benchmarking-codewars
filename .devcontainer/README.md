# Dev Container Configuration

This directory contains the development container configuration for the .NET Benchmarking project.

## What's Included

The dev container provides a complete development environment with:

- **.NET 6.0 SDK**: Full SDK for building and running .NET applications
- **C# Extensions**: Essential VS Code extensions for C# development
- **Git**: Pre-configured Git for version control
- **Automatic Setup**: Dependencies are restored automatically when the container starts

## Configuration Details

### Base Image
Uses the official Microsoft .NET dev container image: `mcr.microsoft.com/devcontainers/dotnet:1-6.0`

This image is:
- Cross-platform compatible (Linux, Windows via WSL2, macOS)
- Maintained by Microsoft
- Includes all necessary .NET SDK tools

### VS Code Extensions
The following extensions are automatically installed:
- `ms-dotnettools.csharp` - C# language support
- `ms-dotnettools.csdevkit` - C# Dev Kit for enhanced development experience
- `formulahendry.dotnet-test-explorer` - Test Explorer for .NET tests
- `jongrant.csharpsortusings` - Automatic using statement organization
- `kreativ-software.csharpextensions` - Additional C# productivity features

### Post-Create Command
After the container is created, `dotnet restore` runs automatically to restore all NuGet packages for the solution.

## Platform Support

### Windows
- Requires Docker Desktop with WSL2 backend
- Or Podman Desktop as an alternative

### Linux
- Requires Docker Engine or Podman
- Works on any Linux distribution with container runtime support

### macOS
- Requires Docker Desktop
- Or Podman Desktop as an alternative

## Customization

To customize the dev container:
1. Edit `devcontainer.json` to add more extensions, features, or change settings
2. Rebuild the container: Command Palette → "Dev Containers: Rebuild Container"

## Troubleshooting

**Container fails to build:**
- Ensure Docker/Podman is running
- Check internet connectivity (needed to download the image)
- Try rebuilding: "Dev Containers: Rebuild Container Without Cache"

**Extensions not working:**
- Reload VS Code window: "Developer: Reload Window"
- Check the extensions are installed in the container context, not locally

**Performance issues:**
- On Windows, ensure the repository is in the WSL2 filesystem for better performance
- Allocate more resources to Docker Desktop in settings
