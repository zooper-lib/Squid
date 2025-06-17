using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Zooper.Squid.Modules;
using Zooper.Squid.Pipeline;

namespace Zooper.Squid.Extensions;

/// <summary>
/// Provides extension methods for working with modules in an ASP.NET Core application.
/// </summary>
public static class ModuleExtensions
{
	/// <summary>
	/// Registers service modules in the application's service collection.
	/// </summary>
	/// <param name="builder">The <see cref="WebApplicationBuilder"/> to add the modules to.</param>
	/// <param name="moduleTypes">The types of the service modules to add.</param>
	/// <returns>The <see cref="WebApplicationBuilder"/> for chaining.</returns>
	/// <example>
	/// <code>
	/// var builder = WebApplication.CreateBuilder(args);
	/// builder.AddServiceModules(typeof(AuthenticationServiceModule), typeof(SwaggerServiceModule));
	/// </code>
	/// </example>
	public static WebApplicationBuilder AddServiceModules(
		this WebApplicationBuilder builder,
		params Type[] moduleTypes)
	{
		foreach (var moduleType in moduleTypes)
		{
			if (Activator.CreateInstance(moduleType) is IServiceModule module)
			{
				module.ConfigureServices(builder);
			}
		}

		return builder;
	}

	/// <summary>
	/// Creates a middleware pipeline builder for explicit middleware ordering.
	/// </summary>
	/// <param name="app">The <see cref="WebApplication"/> to configure.</param>
	/// <returns>A <see cref="MiddlewarePipelineBuilder"/> for configuring middleware.</returns>
	/// <example>
	/// <code>
	/// var app = builder.Build();
	/// app.ConfigureMiddlewarePipeline()
	///    .Add(app => app.UseAuthentication())
	///    .AddModule&lt;SwaggerMiddlewareModule&gt;()
	///    .Build();
	/// </code>
	/// </example>
	public static MiddlewarePipelineBuilder ConfigureMiddlewarePipeline(this WebApplication app)
	{
		return new MiddlewarePipelineBuilder(app);
	}

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
	[Obsolete("Use AddServiceModules instead. This method will be removed in a future version.")]
	public static WebApplicationBuilder AddModules(
		this WebApplicationBuilder builder,
		params Type[] moduleTypes)
	{
#pragma warning disable CS0618 // Type or member is obsolete
		foreach (var moduleType in moduleTypes)
		{
			if (Activator.CreateInstance(moduleType) is not AppModule module) continue;

			module.ConfigureServices(builder);
			builder.Services.AddSingleton(module);
		}
#pragma warning restore CS0618 // Type or member is obsolete

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
	[Obsolete("Use ConfigureMiddlewarePipeline instead for explicit middleware ordering. This method will be removed in a future version.")]
	public static WebApplication UseModules(this WebApplication app)
	{
#pragma warning disable CS0618 // Type or member is obsolete
		var modules = app.Services.GetServices<AppModule>();

		foreach (var module in modules)
		{
			module.ConfigureMiddleware(app);
		}
#pragma warning restore CS0618 // Type or member is obsolete

		return app;
	}
}