using App.Service.Products.Dto;

namespace App.Service.Categories.Dto
{
	public record CategoryWithProductsDto(int Id, string Name, List<ProductDto> Products);
}
