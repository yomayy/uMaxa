using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shop.Application.ProductsAdmin;
using System;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
	[Route("[controller]")]
	[Authorize(Policy = "Manager")]
	public class ProductsController : Controller
	{
		//private const int PageZize = 4;

		[HttpGet("")]
		public IActionResult GetProducts(
				[FromServices] GetProducts getProducts,
				int pageNumber = 1, int pageSize = 1) => 
			Ok(getProducts.Do(pageNumber, pageSize));

		[Route("count")]
		[HttpGet]
		public IActionResult GetCount(
				[FromServices] GetProducts getProducts) {
			int count = getProducts.GetProductsCount().Result;
			return Ok(count);
		}

		[Route("{categoryId}/count")]
		[HttpGet]
		public IActionResult GetCountBuCategoryId(
				Guid? categoryId,
				[FromServices] GetProducts getProducts) {
			int count = getProducts.GetProductsCount(categoryId).Result;
			return Ok(count);
		}

		[HttpGet("{id}")]
		public IActionResult GetProduct(
				Guid id,
				[FromServices] GetProduct getProduct) => 
			Ok(getProduct.Do(id));

		[HttpPost("")]
		public async Task<IActionResult> CreateProduct(
				[FromBody] CreateProduct.Request request,
				[FromServices] CreateProduct createProduct) => 
			Ok((await createProduct.DoAsync(request)));

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(
				Guid id,
				[FromServices] DeleteProduct deleteProduct) => 
			Ok((await deleteProduct.DoAsync(id)));

		[HttpPut("")]
		public async Task<IActionResult> UpdateProduct(
				[FromBody] UpdateProduct.Request request,
				[FromServices] UpdateProduct updateProduct) => 
			Ok((await updateProduct.DoAsync(request)));
	}
}
