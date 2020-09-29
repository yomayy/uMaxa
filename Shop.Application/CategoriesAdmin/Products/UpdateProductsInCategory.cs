using Shop.Domain.BaseModels;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Application.CategoriesAdmin.Products
{
	[Service]
	public class UpdateProductsInCategory
	{
		private readonly ICategoryManager _categoryManager;

		public UpdateProductsInCategory(ICategoryManager categoryManager) {
			_categoryManager = categoryManager;
		}

		public async Task<Response> DoAsync(Request request) {
			var productList = new List<Product>();
			foreach (var product in request?.Products) {
				productList.Add(new Product {
					Id = product.Id,
					Name = product?.Name,
					Description = product?.Description,
					Value = product.Value,
					CategoryId = product?.CategoryId,
					CreatedOn = product?.CreatedOn,
					ModifiedOn = DateTime.UtcNow
				});
			}
			await _categoryManager.UpdateProductsInCategory(productList);
			return new Response {
				Products = request?.Products
			};
		}

		public class ProductViewModel : DbBase
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
			public Guid? CategoryId { get; set; }
		}

		public class Request
		{
			public IEnumerable<ProductViewModel> Products { get; set; }
		}

		public class Response
		{
			public IEnumerable<ProductViewModel> Products { get; set; }
		}
	}
}
