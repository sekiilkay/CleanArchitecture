﻿using App.Application.Contracts.Persistance;
using App.Domain.Options;
using App.Persistance.Categories;
using App.Persistance.Interceptors;
using App.Persistance.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Persistance.Extensions
{
	public static class RepositoryExtensions
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
			{
				var connectionStrings =
					configuration.GetSection(ConnectionStringsOption.Key).Get<ConnectionStringsOption>();

				options.UseSqlServer(connectionStrings!.SqlServer,
					sqlServerOptionsAction =>
					{
						sqlServerOptionsAction.MigrationsAssembly(typeof(PersistanceAssembly).Assembly.FullName);
					});

				options.AddInterceptors(new AuditDbContextInterceptor());
			});
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

			services.AddScoped<IUnitOfWork, UnitOfWork>();
			return services;
		}
	}
}
