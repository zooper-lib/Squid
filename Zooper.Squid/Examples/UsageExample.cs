using Microsoft.AspNetCore.Builder;
using Zooper.Squid.Examples;
using Zooper.Squid.Extensions;

namespace Zooper.Squid.Examples;

/// <summary>
/// Example Program.cs showing how to use the module pattern.
/// </summary>
public class UsageExample
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Register service modules (order doesn't matter here)
		builder.AddServiceModules(
			typeof(ExampleServiceModule),
			typeof(DevelopmentToolsServiceModule)
		// Add more service modules as needed
		);

		var app = builder.Build();

		// Configure middleware with explicit ordering using the pipeline builder
		app.ConfigureMiddlewarePipeline()
			// First: Add exception handling (should be early in pipeline)
			.AddModule<DevelopmentToolsMiddlewareModule>()

			// Then: Add custom middleware
			.Add(app => app.UseRouting())

			// Add module-based middleware
			.AddModule<ExampleMiddlewareModule>()
			// Add endpoints last
			.Add(app => app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", () => "Hello World!");
			}))

			// Build the pipeline
			.Build();

		app.Run();
	}
	/// <summary>
	/// Example showing backward compatibility with legacy modules.
	/// </summary>
	[Obsolete("This example shows legacy module usage for backward compatibility.")]
	public static void OldPatternExample(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Legacy pattern - still works but shows obsolete warnings
#pragma warning disable CS0618 // Type or member is obsolete
		builder.AddModules(
			typeof(OldPatternExampleModule)
		);

		var app = builder.Build();

		app.UseModules();
#pragma warning restore CS0618 // Type or member is obsolete

		app.Run();
	}
}

/// <summary>
/// Example of legacy module pattern for backward compatibility testing.
/// </summary>
[Obsolete("Use IServiceModule and IMiddlewareModule interfaces instead.")]
public sealed class OldPatternExampleModule : Zooper.Squid.Modules.AppModule
{
	public override void ConfigureServices(WebApplicationBuilder builder)
	{
		// Add a simple service registration (no external dependencies)
	}

	public override void ConfigureMiddleware(WebApplication app)
	{
		app.UseRouting();
	}
}
