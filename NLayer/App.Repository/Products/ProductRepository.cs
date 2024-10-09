
using Microsoft.EntityFrameworkCore;

namespace App.Repository.Products
{
	public class ProductRepository(AppDbContext context)
		: GenericRepository<Product, int>(context), IProductRepository
	{
		public Task<List<Product>> GetTopPriceProductsAsync(int count)
		{
			return context.Products
				.OrderByDescending(x => x.Price)
				.Take(count)
				.ToListAsync();
		}
	}
}
