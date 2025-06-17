using Microsoft.AspNetCore.Builder;
using Zooper.Squid.Modules;

namespace Zooper.Squid.Pipeline;

/// <summary>
/// Builder for configuring middleware pipeline with explicit ordering.
/// </summary>
/// <remarks>
/// This builder allows you to explicitly define the order of middleware components,
/// mixing regular middleware configuration with module-based middleware.
/// </remarks>
public class MiddlewarePipelineBuilder
{
	private readonly List<Action<WebApplication>> _middlewareActions = new();
	private readonly WebApplication _app;

	/// <summary>
	/// Initializes a new instance of the <see cref="MiddlewarePipelineBuilder"/> class.
	/// </summary>
	/// <param name="app">The <see cref="WebApplication"/> to configure.</param>
	public MiddlewarePipelineBuilder(WebApplication app)
	{
		_app = app;
	}

	/// <summary>
	/// Adds a middleware configuration action to the pipeline.
	/// </summary>
	/// <param name="configure">The middleware configuration action.</param>
	/// <returns>The <see cref="MiddlewarePipelineBuilder"/> for chaining.</returns>
	/// <example>
	/// <code>
	/// builder.Add(app => app.UseAuthentication());
	/// </code>
	/// </example>
	public MiddlewarePipelineBuilder Add(Action<WebApplication> configure)
	{
		_middlewareActions.Add(configure);
		return this;
	}

	/// <summary>
	/// Adds middleware from a module type to the pipeline.
	/// </summary>
	/// <typeparam name="T">The type of middleware module to add.</typeparam>
	/// <returns>The <see cref="MiddlewarePipelineBuilder"/> for chaining.</returns>
	/// <example>
	/// <code>
	/// builder.AddModule&lt;SwaggerMiddlewareModule&gt;();
	/// </code>
	/// </example>
	public MiddlewarePipelineBuilder AddModule<T>() where T : IMiddlewareModule, new()
	{
		var module = new T();
		_middlewareActions.Add(module.ConfigureMiddleware);
		return this;
	}

	/// <summary>
	/// Adds middleware from a module instance to the pipeline.
	/// </summary>
	/// <param name="module">The middleware module instance to add.</param>
	/// <returns>The <see cref="MiddlewarePipelineBuilder"/> for chaining.</returns>
	/// <example>
	/// <code>
	/// var module = new SwaggerMiddlewareModule();
	/// builder.AddModule(module);
	/// </code>
	/// </example>
	public MiddlewarePipelineBuilder AddModule(IMiddlewareModule module)
	{
		_middlewareActions.Add(module.ConfigureMiddleware);
		return this;
	}

	/// <summary>
	/// Builds and configures the middleware pipeline by executing all added middleware actions.
	/// </summary>
	/// <returns>The configured <see cref="WebApplication"/>.</returns>
	/// <remarks>
	/// This method should be called last in the pipeline configuration chain as it executes
	/// all the middleware configuration actions that were added to the builder.
	/// </remarks>
	public WebApplication Build()
	{
		foreach (var action in _middlewareActions)
		{
			action(_app);
		}
		return _app;
	}
}
