using AutoMapper;

namespace App.Application.Features.Categories
{
	internal class CategoryMapping : Profile
	{
		public CategoryMapping()
		{
			CreateMap<CategoryDto, Category>().ReverseMap();

			CreateMap<Category, CategoryWithProductsDto>().ReverseMap();

			CreateMap<CreateCategoryRequest, Category>().ForMember(dest => dest.Name,
				opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

			CreateMap<UpdateCategoryRequest, Category>().ForMember(dest => dest.Name,
				opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
		}
	}
}
