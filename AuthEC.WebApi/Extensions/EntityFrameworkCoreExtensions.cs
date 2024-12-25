using AuthEC.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthEC.WebApi.Extensions
{
	public static class EntityFrameworkCoreExtensions
	{
		public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration) 
		{
			services.AddDbContext<ApplicationDbContext>(options => 
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
			);
			return services;
		}
	}
}
