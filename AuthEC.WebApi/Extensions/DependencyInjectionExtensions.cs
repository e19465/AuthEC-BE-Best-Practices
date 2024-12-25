using AuthEC.Abstractions.Interfaces;
using AuthEC.Services;

namespace AuthEC.WebApi.Extensions
{
	public static class DependencyInjectionExtensions
	{
		public static IServiceCollection AddServiceLifeTimes(this IServiceCollection services)
		{
			services.AddScoped<IAppUserService, AppUserService>();
			return services;
		}
	}
}
