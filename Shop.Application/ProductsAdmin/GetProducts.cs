using Shop.Domain.Infrastructure;
using System;
using System.Collections.Generic;

namespace Shop.Application.ProductsAdmin
{
	public class GetProducts
	{
		private IProductManager _productManager;

		public GetProducts(IProductManager productManager) {
			_productManager = productManager;
		}

		public IEnumerable<ProductViewModel> Do() {
			return _productManager.GetProductsWithStock(p => new ProductViewModel {
				Id = p.Id,
				Name = p.Name,
				Description = p.Description,
				Value = p.Value
			});
		}

		public class ProductViewModel
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
		}
	}
}
