# Benchmarking Codewars Tasks
This repository is for learning benchmarking via [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet).

## Development Environment

### Dev Container (Recommended)
This project includes a dev container configuration for cross-platform development on Linux and Windows.

**Prerequisites:**
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (Windows/Mac) or [Docker Engine](https://docs.docker.com/engine/install/) (Linux)
- [Visual Studio Code](https://code.visualstudio.com/)
- [Dev Containers extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers)

**Getting Started:**
1. Open the project folder in VS Code
2. When prompted, click "Reopen in Container" or run the command "Dev Containers: Reopen in Container"
3. The container will build and install all necessary dependencies automatically

The dev container includes:
- .NET 6.0 SDK
- C# development tools and extensions
- Git configuration
- Automatic dependency restoration

### Local Development
If you prefer to develop locally, ensure you have .NET 6.0 SDK or later installed.

## Codewars solutions
Own kata solutions are decoreted with `[Benchmark(Baseline = true)]`.
Each `Baseline` method is compared with top 3 kata solutions by memory consumption and execution performance.

## Benchmark run
`dotnet run --project .\Benchmarking\Benchmarking.csproj -c Release`
