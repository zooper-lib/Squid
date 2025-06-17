# Zooper.Squid

<img src="icon.png" alt="Zooper.Squid Logo" width="120" align="right"/>

[![NuGet Version](https://img.shields.io/nuget/v/Zooper.Squid.svg)](https://www.nuget.org/packages/Zooper.Squid/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Zooper.Squid is a lightweight .NET library that simplifies the creation of modular ASP.NET Core applications. It provides a clean and intuitive way to organize your application into independent, reusable modules that can be easily configured and composed.

## Key Features

- **Modular Architecture**: Break down your application into independent, focused modules
- **Separation of Concerns**: Distinct service and middleware module interfaces
- **Explicit Middleware Ordering**: Full control over middleware pipeline execution order
- **Easy Configuration**: Simple and intuitive module registration and configuration
- **Dependency Injection**: Automatic registration of service modules in your DI container
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
// Define your service module
public class UserServiceModule : IServiceModule
{
    public void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
    }
}

// Define your middleware module
public class UserMiddlewareModule : IMiddlewareModule
{
    public void ConfigureMiddleware(WebApplication app)
    {
        app.MapControllers();
    }
}

// Register modules in your Program.cs
var builder = WebApplication.CreateBuilder(args);

// Register your service modules
builder.AddServiceModules(
    typeof(UserServiceModule),
    typeof(AuthenticationServiceModule)
);

var app = builder.Build();

// Configure middleware pipeline with explicit ordering
app.ConfigureMiddlewarePipeline()
    .AddModule<AuthenticationMiddlewareModule>() // Authentication first
    .Add(app => app.UseRouting())                // Then routing
    .AddModule<UserMiddlewareModule>()           // Then user endpoints
    .Build();

app.Run();
```

## Core Concepts

### Service Modules

Service modules handle dependency injection configuration and are completely separate from middleware concerns:

```csharp
public class AuthenticationServiceModule : IServiceModule
{
    public void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();
    }
}
```

### Middleware Modules

Middleware modules handle the request pipeline configuration:

```csharp
public class AuthenticationMiddlewareModule : IMiddlewareModule
{
    public void ConfigureMiddleware(WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
```

### Explicit Middleware Ordering

The middleware pipeline builder allows you to explicitly control the order of middleware execution:

```csharp
app.ConfigureMiddlewarePipeline()
    .Add(app => app.UseExceptionHandler("/Error"))  // Exception handling first
    .AddModule<AuthenticationMiddlewareModule>()    // Then authentication
    .Add(app => app.UseRouting())                   // Then routing
    .AddModule<ApiMiddlewareModule>()               // Then API endpoints
    .Build();
```

### Module Registration

Zooper.Squid provides a simple way to register your service modules:

```csharp
// Register a single service module
builder.AddServiceModules(typeof(AuthenticationServiceModule));

// Register multiple service modules
builder.AddServiceModules(
    typeof(AuthenticationServiceModule),
    typeof(UserServiceModule),
    typeof(LoggingServiceModule)
);
```

## Best Practices

### Module Design

1. **Single Responsibility**: Each module should have a single, well-defined purpose
2. **Separation of Concerns**: Keep service configuration separate from middleware configuration
3. **Clear Boundaries**: Define clear boundaries between modules
4. **Minimal Dependencies**: Keep inter-module dependencies to a minimum

### Module Implementation

1. **Service Modules**: Use IServiceModule for dependency injection configuration
2. **Middleware Modules**: Use IMiddlewareModule for request pipeline configuration
3. **Explicit Ordering**: Use the middleware pipeline builder for explicit middleware ordering
4. **Dependencies**: Use constructor injection for module dependencies
5. **Error Handling**: Implement proper error handling within modules

### Testing

1. **Module Isolation**: Test modules in isolation
2. **Integration Testing**: Test module interactions
3. **Mock Dependencies**: Use mocks for external dependencies

## Examples

Check out the examples in the `Zooper.Squid/Examples` folder for comprehensive examples including:

- Service and middleware module separation
- Authentication service and middleware modules
- Development tools modules
- Explicit middleware pipeline configuration
- Module usage patterns

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Related Projects

- [Zooper.Octopus](https://github.com/zooper-lib/Octopus) - Hexagonal Architecture implementation
- [Zooper.Bee](https://github.com/zooper-lib/Bee) - Fluent workflow framework
- [Zooper.Fox](https://github.com/zooper-lib/Fox) - Functional programming primitives
