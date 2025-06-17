using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zooper.Squid.Modules;

namespace Zooper.Squid.Examples;

/// <summary>
/// Example service module for configuring custom services.
/// </summary>
public sealed class ExampleServiceModule : IServiceModule
{
	public void ConfigureServices(WebApplicationBuilder builder)
	{
		// Example: Add custom services using basic service registration
		// Add custom scoped service
		builder.Services.AddScoped<IExampleService, ExampleService>();
	}
}

/// <summary>
/// Example middleware module for configuring middleware pipeline.
/// </summary>
public sealed class ExampleMiddlewareModule : IMiddlewareModule
{
	public void ConfigureMiddleware(WebApplication app)
	{
		// Add custom middleware
		app.Use(async (context, next) =>
		{
			// Example middleware logic
			await next();
		});
	}
}

/// <summary>
/// Example service module for configuring development tools.
/// </summary>
public sealed class DevelopmentToolsServiceModule : IServiceModule
{
	public void ConfigureServices(WebApplicationBuilder builder)
	{
		// Add services needed for development tools
		if (builder.Environment.IsDevelopment())
		{
			// Add development-specific services here
			builder.Services.AddSingleton<IDevelopmentService, DevelopmentService>();
		}
	}
}

/// <summary>
/// Example middleware module for configuring development tools middleware.
/// </summary>
public sealed class DevelopmentToolsMiddlewareModule : IMiddlewareModule
{
	public void ConfigureMiddleware(WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}
	}
}

// Example service interfaces and implementations for demonstration
public interface IExampleService
{
	Task<string> GetDataAsync();
}

public class ExampleService : IExampleService
{
	public Task<string> GetDataAsync()
	{
		return Task.FromResult("Example data");
	}
}

public interface IDevelopmentService
{
	void LogDevelopmentInfo();
}

public class DevelopmentService : IDevelopmentService
{
	public void LogDevelopmentInfo()
	{
		// Development logging logic
	}
}
