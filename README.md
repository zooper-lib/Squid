# Zooper.Squid

<img src="icon.png" alt="Zooper.Squid Logo" width="120" align="right"/>

[![NuGet Version](https://img.shields.io/nuget/v/Zooper.Squid.svg)](https://www.nuget.org/packages/Zooper.Squid/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Zooper.Squid is a lightweight .NET library that simplifies the creation of modular ASP.NET Core applications. It provides a clean and intuitive way to organize your application into independent, reusable modules that can be easily configured and composed.

## Key Features

- **Modular Architecture**: Break down your application into independent, focused modules
- **Easy Configuration**: Simple and intuitive module registration and configuration
- **Dependency Injection**: Automatic registration of modules in your DI container
- **Flexible Composition**: Mix and match modules to build your application
- **Minimal Dependencies**: Only depends on ASP.NET Core
- **Type-safe**: Leverages C#'s type system for robust module definitions
- **Testable**: Designed with testability in mind from the ground up

## Installation

```bash
dotnet add package Zooper.Squid
```

## Quick Start

Here's a basic example of creating a modular ASP.NET Core application with Zooper.Squid:

```csharp
// Define your module (in your modules layer)
public class UserModule : AppModule
{
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
    }

    public override void ConfigureMiddleware(WebApplication app)
    {
        app.MapControllers();
    }
}

// Register modules in your Program.cs
var builder = WebApplication.CreateBuilder(args);

// Register your modules
builder.AddModules(typeof(UserModule));

var app = builder.Build();

// Configure your modules
app.UseModules();

app.Run();
```

## Core Concepts

### Modules

Modules are self-contained units of functionality that can be independently developed, tested, and deployed:

```csharp
public class AuthenticationModule : AppModule
{
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();
    }

    public override void ConfigureMiddleware(WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
```

### Module Registration

Zooper.Squid provides a simple way to register your modules:

```csharp
// Register a single module
builder.AddModules(typeof(AuthenticationModule));

// Register multiple modules
builder.AddModules(
    typeof(AuthenticationModule),
    typeof(UserModule),
    typeof(LoggingModule)
);
```

## Best Practices

### Module Design

1. **Single Responsibility**: Each module should have a single, well-defined purpose
2. **Encapsulation**: Modules should encapsulate their implementation details
3. **Clear Boundaries**: Define clear boundaries between modules
4. **Minimal Dependencies**: Keep inter-module dependencies to a minimum

### Module Implementation

1. **Configuration**: Use ConfigureServices for service registration
2. **Middleware**: Use ConfigureMiddleware for pipeline configuration
3. **Dependencies**: Use constructor injection for module dependencies
4. **Error Handling**: Implement proper error handling within modules

### Testing

1. **Module Isolation**: Test modules in isolation
2. **Integration Testing**: Test module interactions
3. **Mock Dependencies**: Use mocks for external dependencies

## Examples

Check out the [Zooper.Squid.Examples](./Zooper.Squid.Examples) project for comprehensive examples including:

- Authentication module
- User management module
- Logging module
- API module
- Different module configurations

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Related Projects

- [Zooper.Octopus](https://github.com/zooper-lib/Octopus) - Hexagonal Architecture implementation
- [Zooper.Bee](https://github.com/zooper-lib/Bee) - Fluent workflow framework
- [Zooper.Fox](https://github.com/zooper-lib/Fox) - Functional programming primitives
