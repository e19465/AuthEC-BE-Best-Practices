using System.Security.Claims;
using System.Text;
using AuthEC.DataAccess.Data;
using AuthEC.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AuthEC.WebApi.Extensions
{
	public static class IdentityExtensions
	{

		/// <summary>
		/// This method adds the Identity handlers and stores to the application
		/// </summary>
		/// <param name="services">IServiceCollection services</param>
		/// <returns>IServiceCollection services</returns>
		public static IServiceCollection AddIdentityHandlersAndStores(this IServiceCollection services)
		{
			services.AddIdentityApiEndpoints<AppUser>()
					.AddRoles<IdentityRole>()
					.AddEntityFrameworkStores<ApplicationDbContext>();
			return services;
		}



		/// <summary>
		/// This method configures the Identity options
		/// </summary>
		/// <param name="services">IServiceCollection services</param>
		/// <returns>IServiceCollection services</returns>
		public static IServiceCollection ConfigureIdentityOptions(this IServiceCollection services)
		{
			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequiredLength = 6;
				options.User.RequireUniqueEmail = true;
				options.SignIn.RequireConfirmedEmail = true;
			});
			return services;
		}

		/// <summary>
		/// This method adds the authentication and authorization middlewares to the application
		/// </summary>
		/// <param name="services">IServiceCollection services</param>
		/// <param name="configuration">IConfiguration configuration</param>
		/// <returns>IServiceCollection services</returns>
		public static IServiceCollection AddIdentityAuth(this IServiceCollection services, IConfiguration configuration)
		{
			services
				.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(y =>
				{
					y.SaveToken = false;
					y.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:AccessTokenSecret"]!)),
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						RoleClaimType = "Role",
						ClockSkew = TimeSpan.Zero

					};
				});

			services.AddAuthorization(options =>
			{
				options.FallbackPolicy = new AuthorizationPolicyBuilder()
					.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
					.RequireAuthenticatedUser()
					.Build();

				options.AddPolicy("HasLibraryId", policy => policy.RequireClaim("LibraryId"));
				options.AddPolicy("FemalesOnly", policy => policy.RequireClaim("Gender", "Female"));
				options.AddPolicy("AgeUnderTenOnly", policy => policy.RequireAssertion(context =>
					Int32.Parse(context.User.Claims.First(x => x.Type == "Age").Value) < 10
				));
			});

			return services;
		}



		/// <summary>
		/// This method adds the authentication and authorization middlewares to the application
		/// </summary>
		/// <param name="app">WebApplication app</param>
		/// <returns>WebApplication app</returns>
		public static WebApplication AddIdentityAuthMiddlewares(this WebApplication app)
		{
			app.UseAuthentication();
			app.UseAuthorization();
			return app;
		}
	}
}
