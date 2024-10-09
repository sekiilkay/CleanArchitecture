using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using App.Application.Contracts.Persistance;
using App.Application.Features.Products;
using App.Application.Features.Categories;

namespace App.Application.Extensions
{
	public static class ServiceExtension
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<ICategoryService, CategoryService>();
			//services.AddScoped(typeof(NotFoundFilter<,>));


			services.AddFluentValidationAutoValidation();

			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddAutoMapper(Assembly.GetExecutingAssembly());


			//services.AddExceptionHandler<CriticalExceptionHandler>();
			//services.AddExceptionHandler<GlobalExceptionHandler>();

			return services;
		}
	}
}
