using CleanApp.API.Filters;
using App.Persistance.Extensions;
using App.Application.Extensions;
using App.Application.Contracts.Caching;
using App.Caching;
using CleanApp.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
	.AddControllerWithFiltersExt()
	.AddSwaggerGenExt()
	.AddExceptionHandlerExt()
	.AddCachingExt();

builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);
	//.AddBusExt(builder.Configuration);

var app = builder.Build();

app.UseConfigurePipelineExt();

app.MapControllers();

app.Run();
