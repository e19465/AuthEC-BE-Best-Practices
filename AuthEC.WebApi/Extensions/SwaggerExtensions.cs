using Microsoft.OpenApi.Models;

namespace AuthEC.WebApi.Extensions
{
	public static class SwaggerExtensions
	{


		/// <summary>
		/// This method adds the Swagger services to the application
		/// </summary>
		/// <param name="services">IServiceCollection services</param>
		/// <returns>IServiceCollection services</returns>
		public static IServiceCollection AddSwagger(this IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					Description = "JWT Authorization Header Using The Bearer Scheme"
				});
				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new List<string>()
					}
				});
			});
			return services;
		}



		/// <summary>
		/// This method adds the Swagger UI to the application
		/// </summary>
		/// <param name="application">WebApplication type application</param>
		/// <returns>WebApplication application</returns>
		public static WebApplication ConfigureSwagger(this WebApplication application) 
		{
			if (application.Environment.IsDevelopment())
			{
				application.UseSwagger();
				application.UseSwaggerUI();
			}
			return application;
		}
	}
}
