using App.Service.Categories.Create;
using App.Service.Categories.Dto;
using App.Service.Categories.Update;

namespace App.Service.Categories
{
	public interface ICategoryService
	{
		Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId);
		Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync();
		Task<ServiceResult<List<CategoryDto>>> GetAllListAsync();
		Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);
		Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request);
		Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request);
		Task<ServiceResult> DeleteAsync(int id);
	}
}
