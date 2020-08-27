using Shop.Database;
using Shop.Domain.BaseModels;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
	public class CreateStock
	{
		private ApplicationDbContext _context;

		public CreateStock(ApplicationDbContext context) {
			_context = context;
		}

		public async Task<Response> Do(Request request) {
			var stock = new Stock {
				Description = request?.Description,
				Quantity = request.Quantity,
				ProductId = request.ProductId
			};

			_context.Stocks.Add(stock);
			await _context.SaveChangesAsync();

			return new Response {
				Id = stock.Id,
				Description = stock?.Description,
				Quantity = stock.Quantity,
				CreatedOn = stock?.CreatedOn,
				ModifiedOn = stock?.ModifiedOn
			};
		}

		public class Request
		{
			public string Description { get; set; }
			public int Quantity { get; set; }
			public Guid ProductId { get; set; }
		}

		public class Response : DbBase
		{
			public string Description { get; set; }
			public int Quantity { get; set; }
		}
	}
}
