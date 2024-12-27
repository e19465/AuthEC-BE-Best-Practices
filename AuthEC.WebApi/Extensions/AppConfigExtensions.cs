using AuthEC.WebApi.Config;
using AuthEC.WebApi.Utils;

namespace AuthEC.WebApi.Extensions
{
    public static class AppConfigExtensions
	{

		/// <summary>
		/// This method configures the CORS policy
		/// </summary>
		/// <param name="app">WebApplication app</param>
		/// <param name="configuration">IConfiguration configuration</param>
		/// <returns>WebApplication app</returns>
		public static WebApplication ConfigureCORS(this WebApplication app, IConfiguration configuration)
		{
			app.UseCors(options => CorsOptionsConfig.ConfigureCorsPolicy(options));
			return app;
		}



		/// <summary>
		/// This method adds the AppSettings to the application
		/// </summary>
		/// <param name="services">IServiceCollection services</param>
		/// <param name="configuration">IConfiguration configuration</param>
		/// <returns>IServiceCollection services</returns>
		public static IServiceCollection AddAppConfig(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
			return services;
		}
	}
}
