using Shop.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.Products
{
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

				StockCount = p.Stocks.Sum(y => y.Quantity)
			});
		}

		public class ProductViewModel
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public string Value { get; set; }
			public int StockCount { get; set; }
		}
	}
}
