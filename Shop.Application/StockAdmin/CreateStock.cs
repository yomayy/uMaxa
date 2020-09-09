using Shop.Domain.BaseModels;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
	[Service]
	public class CreateStock
	{
		private readonly IStockManager _stockManager;

		public CreateStock(IStockManager stockManager) {
			_stockManager = stockManager;
		}

		public async Task<Response> Do(Request request) {
			var stock = new Stock {
				Description = request?.Description,
				Quantity = request.Quantity,
				ProductId = request.ProductId
			};

			await _stockManager.CreateStock(stock);

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
