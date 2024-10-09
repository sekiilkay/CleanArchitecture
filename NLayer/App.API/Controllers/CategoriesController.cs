﻿using App.Repository.Categories;
using App.Repository.Products;
using App.Service.Categories;
using App.Service.Categories.Create;
using App.Service.Categories.Dto;
using App.Service.Categories.Update;
using App.Service.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class CategoriesController(ICategoryService categoryService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories() => CreateActionResult(await categoryService.GetAllListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id) =>
            CreateActionResult(await categoryService.GetByIdAsync(id));


        [HttpGet("products")]
        public async Task<IActionResult> GetCategoryWithProducts() =>
            CreateActionResult(await categoryService.GetCategoryWithProductsAsync());

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetCategoryWithProducts(int id) =>
            CreateActionResult(await categoryService.GetCategoryWithProductsAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request) =>
            CreateActionResult(await categoryService.CreateAsync(request));


        [ServiceFilter(typeof(NotFoundFilter<Category, int>))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequest request) =>
            CreateActionResult(await categoryService.UpdateAsync(id, request));


        [ServiceFilter(typeof(NotFoundFilter<Category, int>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id) =>
            CreateActionResult(await categoryService.DeleteAsync(id));
    }
}