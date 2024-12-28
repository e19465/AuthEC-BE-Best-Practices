using AuthEC.Abstractions.Interfaces;
using AuthEC.Services;

namespace AuthEC.WebApi.Extensions
{
	public static class DependencyInjectionExtensions
	{

		/// <summary>
		/// This method adds the service lifetimes to the application (Scoped, Transient, Singleton)
		/// </summary>
		/// <param name="services">IServiceCollection services</param>
		/// <returns>IServiceCollection services</returns>
		public static IServiceCollection AddServiceLifeTimes(this IServiceCollection services)
		{
			services.AddScoped<IAppUserService, AppUserService>();
			services.AddScoped<IAccountService, AccountService>();
			return services;
		}
	}
}
