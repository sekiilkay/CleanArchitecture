using App.Application.Contracts.Persistance;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Persistance.Categories
{
	public class CategoryRepository(AppDbContext context)
		: GenericRepository<Category, int>(context), ICategoryRepository
	{
		public Task<Category?> GetCategoryWithProductsAsync(int id)
		{
			return context.Categories
				.Include(x => x.Products)
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public Task<List<Category>> GetCategoryWithProductsAsync()
		{
			return Context.Categories
				.Include(x => x.Products)
				.ToListAsync();
		}
	}
}
