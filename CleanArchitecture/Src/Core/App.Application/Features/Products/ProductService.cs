using App.Application.Contracts.Caching;
using App.Application.Contracts.Persistance;
using App.Application.Features.Products;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;
using App.Domain.Entities;
using AutoMapper;
using System.Net;

namespace App.Application.Contracts.Persistance
{
    public class ProductService(
		IProductRepository productRepository,
		IMapper mapper,
		IUnitOfWork unitOfWork,
		ICacheService cacheService
		) : IProductService
	{
		private const string ProductListCacheKey = "ProductListCacheKey";
		public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
		{
			var anyProduct = await productRepository.AnyAsync(x => x.Name == request.Name);

			if (anyProduct)
			{
				return ServiceResult<CreateProductResponse>.Fail("Ürün ismi veritabanında bulunmaktadır.", HttpStatusCode.NotFound);
			}

			var product = mapper.Map<Product>(request);
			await productRepository.AddAsync(product);
			await unitOfWork.SaveChangesAsync();

			return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), $"api/products/{product.Id}");
		}

		public async Task<ServiceResult> DeleteAsync(int id)
		{
			var product = await productRepository.GetByIdAsync(id);

			productRepository.Delete(product!);
			await unitOfWork.SaveChangesAsync();

			return ServiceResult.Success(HttpStatusCode.NoContent);
		}

		public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
		{
			// Cache Aside Design Pattern

			// var productListAsCached = await cacheService.GetAsync<List<ProductDto>>(ProductListCacheKey);

			// if (productListAsCached is not null) 
				// return ServiceResult<List<ProductDto>>.Success(productListAsCached);


			var products = await productRepository.GetAllAsync();
			
			var productsAsDto = mapper.Map<List<ProductDto>>(products);

			// await cacheService.AddAsync(ProductListCacheKey, productsAsDto, TimeSpan.FromMinutes(1));

			return ServiceResult<List<ProductDto>>.Success(productsAsDto);
		}

		public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
		{
			var product = await productRepository.GetByIdAsync(id);

			if (product is null)
				return ServiceResult<ProductDto?>.Fail("Ürün bulunamadı!", HttpStatusCode.NotFound);
		
			var productAsDto = mapper.Map<ProductDto>(product);

			return ServiceResult<ProductDto?>.Success(productAsDto);
		}

		public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
		{
			var products = await productRepository.GetAllPagedAsync(pageNumber, pageSize);

			var productsAsDto = mapper.Map<List<ProductDto>>(products);

			return ServiceResult<List<ProductDto>>.Success(productsAsDto);
		}

		public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
		{
			var products = await productRepository.GetTopPriceProductsAsync(count);

			var productsAsDto = mapper.Map<List<ProductDto>>(products);

			return new ServiceResult<List<ProductDto>>()
			{
				Data = productsAsDto
			};
		}

		public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
		{
			var isProductNameExist =
				await productRepository.AnyAsync(x => x.Name == request.Name && x.Id != id);

			if (isProductNameExist)
			{
				return ServiceResult.Fail("Ürün ismi veritabanında bulunmaktadır.", HttpStatusCode.NotFound);
			}

			var product = mapper.Map<Product>(request);
			product.Id = id;

			productRepository.Update(product);
			await unitOfWork.SaveChangesAsync();

			return ServiceResult.Success(HttpStatusCode.NoContent);
		}

		public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
		{
			var product = await productRepository.GetByIdAsync(request.ProductId);

			if (product is null)
			{
				return ServiceResult.Fail("Ürün bulunamadı!", HttpStatusCode.NotFound);
			}

			product.Stock = request.Quantity;

			productRepository.Update(product);
			await unitOfWork.SaveChangesAsync();

			return ServiceResult.Success(HttpStatusCode.NoContent);
		}
	}
}
 