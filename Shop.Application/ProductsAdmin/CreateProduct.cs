using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
	[Service]
	public class CreateProduct
	{
		private IProductManager _productManager;

		public CreateProduct(IProductManager productManager) {
			_productManager = productManager;
		}

		public async Task<Response> DoAsync(Request request) {
			var product = new Product {
				Name = request?.Name,
				Description = request?.Description,
				Value = request.Value,
				CategoryId = request?.Category?.Id
			};
			if(await _productManager.CreateProduct(product) <= 0) {
				throw new Exception("Failed to create product");
			}
			//
			return new Response {
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Value = product.Value,
				Category = new CategoryViewModel {
					Id = request?.Category?.Id,
					Name = request?.Category?.Name
				}
			};
		}
		public class Request
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
			public CategoryViewModel Category { get; set; } = null;
		}

		public class Response
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
			public CategoryViewModel Category { get; set; }
		}

		public class CategoryViewModel
		{
			public Guid? Id { get; set; }
			public string Name { get; set; }
		}
	}
}
