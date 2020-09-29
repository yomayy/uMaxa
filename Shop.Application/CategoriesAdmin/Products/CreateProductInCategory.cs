using Shop.Domain.BaseModels;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Shop.Application.CategoriesAdmin.Products
{
	[Service]
	public class CreateProductInCategory
	{
		private readonly IProductManager _productManager;

		public CreateProductInCategory(IProductManager productManager) {
			_productManager = productManager;
		}

		public async Task<Response> DoAsync(Request request) {
			var product = new Product {
				Name = request?.Name,
				Description = request?.Description,
				Value = request.Value,
				CategoryId = request?.CategoryId,
				ProductImage = request?.Image
			};
			await _productManager.CreateProduct(product);
			return new Response {
				Id = product.Id,
				CreatedOn = product?.CreatedOn,
				ModifiedOn = product?.ModifiedOn,
				Name = product?.Name,
				Description = product?.Description,
				Value = product.Value,
				Image = product?.ProductImage,
				CategoryId = product?.CategoryId
			};
		}

		public class Request
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
			public string Image { get; set; }
			public Guid? CategoryId { get; set; }
		}

		public class Response : DbBase
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
			public string Image { get; set; }
			public Guid? CategoryId { get; set; }
		}
	}
}
