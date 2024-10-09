namespace App.Repository.Categories
{
	public interface ICategoryRepository : IGenericRepository<Category, int>
	{
		Task<Category?> GetCategoryWithProductsAsync(int id);
		IQueryable<Category> GetCategoryWithProducts();
	}
}
