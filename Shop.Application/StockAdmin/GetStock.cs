using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.StockAdmin
{
	public class GetStock
	{
		private ApplicationDbContext _context;

		public GetStock(ApplicationDbContext context) {
			_context = context;
		}

		public IEnumerable<ProductViewModel> Do() {
			var stock = _context?.Products
				?.Include(x => x.Stocks)
				?.Select(x => new ProductViewModel {
					Id = x.Id,
					Description = x.Description,
					Stocks = x.Stocks
					.Select(y => new StockViewModel {
						Id = y.Id,
						Description = y.Description,
						Quantity = y.Quantity,
						CreatedOn = y.CreatedOn,
						ModifiedOn = y.ModifiedOn
					})
				})
				?.ToList();
			return stock;
		}

		public class StockViewModel : DbBase
		{
			public string Description { get; set; }
			public int Quantity { get; set; }
			public Guid ProductId { get; set; }
		}

		public class ProductViewModel : DbBase
		{
			public string Description { get; set; }
			public IEnumerable<StockViewModel> Stocks { get; set; }
		}
	}
}
