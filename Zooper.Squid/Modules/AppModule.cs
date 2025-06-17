using Microsoft.AspNetCore.Builder;
using Zooper.Squid.Extensions;

namespace Zooper.Squid.Modules;

/// <summary>
/// Marker interface for all module types in the application.
/// </summary>
/// <remarks>
/// This interface serves as a common base for all module interfaces and provides type safety
/// when working with modules in the application.
/// </remarks>
public interface IModule
{
}

/// <summary>
/// Defines a module that configures services in the dependency injection container.
/// </summary>
public interface IServiceModule : IModule
{
	/// <summary>
	/// Configures services required by this module.
	/// </summary>
	/// <param name="builder">The <see cref="WebApplicationBuilder"/> used to configure the application.</param>
	void ConfigureServices(WebApplicationBuilder builder);
}

/// <summary>
/// Defines a module that configures middleware in the application pipeline.
/// </summary>
public interface IMiddlewareModule : IModule
{
	/// <summary>
	/// Configures the middleware pipeline for this module.
	/// </summary>
	/// <param name="app">The <see cref="WebApplication"/> used to configure the middleware pipeline.</param>
	void ConfigureMiddleware(WebApplication app);
}

/// <summary>
/// Base class for creating modular components in an ASP.NET Core application.
/// </summary>
/// <remarks>
/// This class provides a foundation for creating modular applications by allowing each module to configure
/// its own services and middleware. Modules can be added to the application using the <see cref="ModuleExtensions"/> class.
/// </remarks>
[Obsolete("Use IServiceModule and IMiddlewareModule interfaces instead. This class will be removed in a future version.")]
public abstract class AppModule
{
	/// <summary>
	/// Configures services required by this module.
	/// </summary>
	/// <param name="builder">The <see cref="WebApplicationBuilder"/> used to configure the application.</param>
	/// <remarks>
	/// Override this method to add services to the dependency injection container.
	/// This method is called during application startup before the middleware pipeline is configured.
	/// </remarks>
	public virtual void ConfigureServices(WebApplicationBuilder builder) { }

	/// <summary>
	/// Configures the middleware pipeline for this module.
	/// </summary>
	/// <param name="app">The <see cref="WebApplication"/> used to configure the middleware pipeline.</param>
	/// <remarks>
	/// Override this method to add middleware to the application's request pipeline.
	/// This method is called during application startup after services have been configured.
	/// </remarks>
	public virtual void ConfigureMiddleware(WebApplication app) { }
}