using Shop.Database;
using Shop.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.StockAdmin
{
	public class GetStocks
	{
		private ApplicationDbContext _context;

		public GetStocks(ApplicationDbContext context) {
			_context = context;
		}

		public IEnumerable<StockViewModel> Do(Guid productId) {
			var stock = _context?.Stocks
				.Where(x => x.ProductId == productId)
				?.Select(x => new StockViewModel {
					Id = x.Id,
					Description = x.Description,
					Quantity = x.Quantity,
					CreatedOn = x.CreatedOn,
					ModifiedOn = x.ModifiedOn
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
	}
}
