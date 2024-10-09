using App.Application.Contracts.Persistance;
using App.Repository.Categories;
using App.Repository.Interceptors;
using App.Repository.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Repository.Extensions
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
						sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
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
