﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.ProductsAdmin;
using System;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
	[Route("[controller]")]
	[Authorize(Policy = "Manager")]
	public class ProductsController : Controller
	{
		[HttpGet("")]
		public IActionResult GetProducts([FromServices] GetProducts getProducts) => 
			Ok(getProducts.Do());

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

		// For paging:

		public IActionResult Index(
				[FromServices] Application.Products.GetProducts getProducts,
				int pageNumber = 1, int pageSize = 2) {
			var products = getProducts.Do(pageNumber, pageSize);
			return View();
		}
	}
}
