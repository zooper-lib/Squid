using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Zooper.Squid.Modules;

/// <summary>
/// Base class for creating modular components in an ASP.NET Core application.
/// </summary>
/// <remarks>
/// This class provides a foundation for creating modular applications by allowing each module to configure
/// its own services and middleware. Modules can be added to the application using the <see cref="ModuleExtensions"/> class.
/// </remarks>
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