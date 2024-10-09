using App.Application.Contracts.Persistance;
using App.Repository;
using App.Repository.Products;
using App.Service.Products.Create;
using App.Service.Products.Dto;
using App.Service.Products.Update;
using App.Service.Products.UpdateStock;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Service.Products
{
	public class ProductService(
		IProductRepository productRepository,
		IMapper mapper,
		IUnitOfWork unitOfWork
		) : IProductService
	{
		public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
		{
			var anyProduct = await productRepository.Where(x => x.Name == request.Name).AnyAsync();

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
			var products = await productRepository.GetAll().ToListAsync();
			
			var productsAsDto = mapper.Map<List<ProductDto>>(products);

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
			var products = await productRepository.GetAll()
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

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
				await productRepository.Where(x => x.Name == request.Name && x.Id != id).AnyAsync();

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
