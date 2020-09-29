using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;

namespace Shop.Application.ProductsAdmin
{
	[Service]
	public class GetProducts
	{
		private IProductManager _productManager;

		public GetProducts(IProductManager productManager) {
			_productManager = productManager;
		}

		public IEnumerable<ProductViewModel> Do() {
			var products = _productManager.GetProductsWithStock(p => new ProductViewModel {
				Id = p.Id,
				Name = p.Name,
				Description = p.Description,
				Value = p.Value,
				//CategoryId = p?.CategoryId
				Category = new CategoryViewModel {
					Id = p?.Category?.Id,
					Name = p?.Category?.Name
				}
			});
			return products;
		}

		public class ProductViewModel
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
			//public Guid? CategoryId { get; set; }
			public CategoryViewModel Category { get; set; } = null;
		}

		public class CategoryViewModel
		{
			public Guid? Id { get; set; }
			public string Name { get; set; } //= null;
		}
	}
}
