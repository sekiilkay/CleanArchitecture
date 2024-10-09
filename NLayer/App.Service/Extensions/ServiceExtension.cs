using App.Service.Categories;
using App.Service.ExceptionHandlers;
using App.Service.Filters;
using App.Service.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace App.Service.Extensions
{
	public static class ServiceExtension
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped(typeof(NotFoundFilter<,>));


			services.AddFluentValidationAutoValidation();

			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddAutoMapper(Assembly.GetExecutingAssembly());


			services.AddExceptionHandler<CriticalExceptionHandler>();
			services.AddExceptionHandler<GlobalExceptionHandler>();

			return services;
		}
	}
}
