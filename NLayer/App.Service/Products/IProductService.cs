using App.Service.Products.Create;
using App.Service.Products.Dto;
using App.Service.Products.Update;
using App.Service.Products.UpdateStock;

namespace App.Service.Products
{
	public interface IProductService
	{
		Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
		Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
		Task<ServiceResult<List<ProductDto>>> GetAllListAsync();
		Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize);
		Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
		Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request);
		Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request);
		Task<ServiceResult> DeleteAsync(int id);
	}
}
