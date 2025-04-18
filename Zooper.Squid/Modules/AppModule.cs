using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Zooper.Squid.Modules;

public abstract class AppModule
{
	public virtual void ConfigureServices(WebApplicationBuilder builder) { }
	public virtual void ConfigureMiddleware(WebApplication app) { }
}