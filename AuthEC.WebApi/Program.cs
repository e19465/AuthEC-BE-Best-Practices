using AuthEC.Entities;
using AuthEC.WebApi.Extensions;


// Define builder for web api application
var builder = WebApplication.CreateBuilder(args);



// Add Controllers
builder.Services.AddControllers();




// Inject the services through Extension methods
builder.Services.AddSwagger()
				.AddDbContext(builder.Configuration)
				.AddAppConfig(builder.Configuration)
				.AddIdentityHandlersAndStores()
				.ConfigureIdentityOptions()
				.AddIdentityAuth(builder.Configuration)
				.AddServiceLifeTimes();




// Define the web api application(app)
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}



// Add middlewares
app.UseHttpsRedirection();
app.MapControllers();
app.ConfigureSwagger()
   .ConfigureCORS(builder.Configuration)
   .AddIdentityAuthMiddlewares();



// Mapping  the Identity API endpoints
app.MapGroup("/api/user")
   .MapIdentityApi<AppUser>();



// Start application
app.Run();
