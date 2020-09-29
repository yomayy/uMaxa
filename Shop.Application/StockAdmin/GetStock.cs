using Shop.Domain.BaseModels;
using Shop.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.StockAdmin
{
	[Service]
	public class GetStock
	{
		private IProductManager _productManager;

		public GetStock(IProductManager productManager) {
			_productManager = productManager;
		}

		public IEnumerable<ProductViewModel> Do() {
			return _productManager.GetProductsWithStock(x => 
				new ProductViewModel {
					Id = x.Id,
					Description = x.Description,
					ProductName = x.Name,
					Image = x?.ProductImage,
					Stocks = x.Stocks
						.Select(y => new StockViewModel {
							Id = y.Id,
							Description = y.Description,
							Quantity = y.Quantity,
							CreatedOn = y.CreatedOn,
							ModifiedOn = y.ModifiedOn
						})
				});
		}

		public class StockViewModel : DbBase
		{
			public string Description { get; set; }
			public int Quantity { get; set; }
			public Guid ProductId { get; set; }
		}

		public class ProductViewModel : DbBase
		{
			public string ProductName { get; set; }
			public string Description { get; set; }
			public string Image { get; set; }
			public IEnumerable<StockViewModel> Stocks { get; set; }
		}
	}
}
