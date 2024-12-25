using Microsoft.OpenApi.Models;

namespace AuthEC.WebApi.Extensions
{
	public static class SwaggerExtensions
	{

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
