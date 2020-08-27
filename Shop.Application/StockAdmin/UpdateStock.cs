using Shop.Database;
using Shop.Domain.BaseModels;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
	public class UpdateStock
	{
		private ApplicationDbContext _context;

		public UpdateStock(ApplicationDbContext context) {
			_context = context;
		}

		public async Task<Response> Do(Request request) {

			var stocks = new List<Stock>();

			foreach (var stock in request?.Stocks) {
				stocks.Add(new Stock {
					Id = stock.Id,
					Description = stock?.Description,
					Quantity = stock.Quantity,
					ProductId = stock.ProductId,
					CreatedOn = stock?.CreatedOn,
					ModifiedOn = stock?.ModifiedOn
				});
			}

			_context.Stocks.UpdateRange(stocks);
			await _context.SaveChangesAsync();

			return new Response {
				Stocks = request?.Stocks
			};
		}

		public class StockViewModel : DbBase
		{
			public string Description { get; set; }
			public int Quantity { get; set; }
			public Guid ProductId { get; set; }
		}

		public class Request
		{
			public IEnumerable<StockViewModel> Stocks { get; set; }
		}

		public class Response
		{
			public IEnumerable<StockViewModel> Stocks { get; set; }
		}
	}
}
