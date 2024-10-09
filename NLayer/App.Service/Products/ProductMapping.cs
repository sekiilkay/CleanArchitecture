using App.Repository.Products;
using App.Service.Products.Create;
using App.Service.Products.Dto;
using App.Service.Products.Update;
using AutoMapper;

namespace App.Service.Products
{
	public class ProductMapping : Profile
	{
		public ProductMapping()
		{
			CreateMap<Product, ProductDto>().ReverseMap();

			CreateMap<CreateProductRequest, Product>().ForMember(dest => dest.Name,
				opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

			CreateMap<UpdateProductRequest, Product>().ForMember(dest => dest.Name,
				opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
		}
	}
}
