using Shop.Domain.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
	[Service]
	public class UpdateProduct
	{
		private IProductManager _productManager;

		public UpdateProduct(IProductManager productManager) {
			_productManager = productManager;
		}

		public async Task<Response> DoAsync(Request request) {

			var product = _productManager.GetProductById(request.Id, x => x);

			product.Name = request.Name;
			product.Description = request.Description;
			product.Value = request.Value;
			product.ModifiedOn = DateTime.UtcNow;
			product.CategoryId = request.Category.Id;
			product.ProductImage = request.Image;

			await _productManager.UpdateProduct(product);

			return new Response {
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Value = product.Value,
				Image = product?.ProductImage,
				ModifiedOn = product.ModifiedOn,
				Category = new CategoryViewModel {
					Id = request?.Category?.Id,
					Name = request?.Category?.Name
				}
			};
		}
		public class Request
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
			public string Image { get; set; }
			public CategoryViewModel Category { get; set; }
		}

		public class Response
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
			public string Image { get; set; }
			public DateTime? ModifiedOn { get; set; }
			public CategoryViewModel Category { get; set; }
		}

		public class CategoryViewModel
		{
			public Guid? Id { get; set; }
			public string Name { get; set; }
		}
	}
}
