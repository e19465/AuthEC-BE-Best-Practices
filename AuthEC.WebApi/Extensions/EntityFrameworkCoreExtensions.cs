using AuthEC.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthEC.WebApi.Extensions
{
	public static class EntityFrameworkCoreExtensions
	{

		/// <summary>
		/// This method adds the DbContext to the application
		/// </summary>
		/// <param name="services">IServiceCollection services</param>
		/// <param name="configuration">IConfiguration configuration</param>
		/// <returns>IServiceCollection services</returns>
		public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration) 
		{
			services.AddDbContext<ApplicationDbContext>(options => 
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
			);
			return services;
		}
	}
}
