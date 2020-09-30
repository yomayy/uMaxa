using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.CategoriesAdmin;
using Shop.Application.CategoriesAdmin.Products;
using System;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
	[Route("[controller]")]
	[Authorize(Policy = "Manager")]
	public class CategoriesController : Controller
	{
		[HttpGet("")]
		public IActionResult GetCategories([FromServices] GetCategories getCategories) =>
			Ok(getCategories.Do());

		[HttpGet("{id}")]
		public IActionResult GetCategory(
				Guid id,
				[FromServices] GetCategory getCategory) =>
			Ok(getCategory.Do(id));

		[HttpPost("")]
		public async Task<IActionResult> CreateCategory(
				[FromBody] CreateCategory.Request request,
				[FromServices] CreateCategory createCategory) =>
			Ok((await createCategory.DoAsync(request)));

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(
				Guid id,
				[FromServices] DeleteCategory deleteCategory) =>
			Ok((await deleteCategory.DoAsync(id)));

		[HttpPut("")]
		public async Task<IActionResult> UpdateCategory(
				[FromBody] UpdateCategory.Request request,
				[FromServices] UpdateCategory updateCategory) =>
			Ok((await updateCategory.DoAsync(request)));

		// Product for category

		[HttpPost("{id}/products")]
		public async Task<IActionResult> CreateProductInCategory(
				[FromBody] CreateProductInCategory.Request request,
				[FromServices] CreateProductInCategory createProductInCategory) =>
			Ok((await createProductInCategory.DoAsync(request)));

		[HttpPut("{id}/products")]
		public async Task<IActionResult> UpdateProductsInCategory(
				[FromBody] UpdateProductsInCategory.Request request,
				[FromServices] UpdateProductsInCategory updateProductsInCategory) =>
			Ok((await updateProductsInCategory.DoAsync(request)));

		[HttpGet("{id}/products")]
		public IActionResult GetProductsInCategory(
				string categoryId,
				[FromServices] GetProductsInCategory getProductsInCategory) =>
			Ok(getProductsInCategory.Do(categoryId));
	}
}
