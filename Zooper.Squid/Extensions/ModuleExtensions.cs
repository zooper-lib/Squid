using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Zooper.Squid.Modules;

namespace Zooper.Squid.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="AppModule"/> instances in an ASP.NET Core application.
/// </summary>
public static class ModuleExtensions
{
	/// <summary>
	/// Registers and configures the specified modules in the application's service collection.
	/// </summary>
	/// <param name="builder">The <see cref="WebApplicationBuilder"/> to add the modules to.</param>
	/// <param name="moduleTypes">The types of the modules to add to the application.</param>
	/// <returns>The <see cref="WebApplicationBuilder"/> for chaining.</returns>
	/// <example>
	/// <code>
	/// var builder = WebApplication.CreateBuilder(args);
	/// builder.AddModules(typeof(MyModule), typeof(AnotherModule));
	/// </code>
	/// </example>
	public static WebApplicationBuilder AddModules(
		this WebApplicationBuilder builder,
		params Type[] moduleTypes)
	{
		foreach (var moduleType in moduleTypes)
		{
			if (Activator.CreateInstance(moduleType) is not AppModule module) continue;

			module.ConfigureServices(builder);
			builder.Services.AddSingleton(module);
		}

		return builder;
	}

	/// <summary>
	/// Configures the middleware pipeline for all registered modules.
	/// </summary>
	/// <param name="app">The <see cref="WebApplication"/> to configure.</param>
	/// <returns>The <see cref="WebApplication"/> for chaining.</returns>
	/// <example>
	/// <code>
	/// var app = builder.Build();
	/// app.UseModules();
	/// </code>
	/// </example>
	public static WebApplication UseModules(this WebApplication app)
	{
		var modules = app.Services.GetServices<AppModule>();

		foreach (var module in modules)
		{
			module.ConfigureMiddleware(app);
		}

		return app;
	}
}