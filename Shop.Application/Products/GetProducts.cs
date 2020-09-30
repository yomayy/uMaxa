using Shop.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.Products
{
	[Service]
	public class GetProducts
	{
		private IProductManager _productManager;

		public GetProducts(IProductManager productManager) {
			_productManager = productManager;
		}

		public IEnumerable<ProductViewModel> Do() {
			return _productManager.GetProductsWithStock(p => new ProductViewModel {
				Name = p.Name,
				Description = p.Description,
				Value = p.Value.GetValueString(),
				Image = p?.ProductImage,
				StockCount = p.Stocks.Sum(y => y.Quantity)
			});
		}

		public IEnumerable<ProductViewModel> Do(string categoryId) {
			Guid? cid = Guid.Parse(categoryId);
			return _productManager.GetProductByCategoryId(cid, p => new ProductViewModel {
				Name = p.Name,
				Description = p.Description,
				Value = p.Value.GetValueString(),
				Image = p?.ProductImage,
				StockCount = p.Stocks.Sum(y => y.Quantity)
			});
		}

		public IEnumerable<ProductViewModel> Do(
				int pageNumber = 1, int pageSize = 2) {
			return _productManager.GetProductsWithStock(p => new ProductViewModel {
				Name = p.Name,
				Description = p.Description,
				Value = p.Value.GetValueString(),
				Image = p?.ProductImage,
				StockCount = p.Stocks.Sum(y => y.Quantity)
			});
		}

		public class ProductViewModel
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public string Value { get; set; }
			public string Image { get; set; }
			public int StockCount { get; set; }
		}
	}
}
