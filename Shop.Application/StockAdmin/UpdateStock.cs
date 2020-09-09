using Shop.Domain.BaseModels;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
	[Service]
	public class UpdateStock
	{
		private IStockManager _stockManager;

		public UpdateStock(IStockManager stockManager) {
			_stockManager = stockManager;
		}

		public async Task<Response> Do(Request request) {

			var stockList = new List<Stock>();

			foreach (var stock in request?.Stocks) {
				stockList.Add(new Stock {
					Id = stock.Id,
					Description = stock?.Description,
					Quantity = stock.Quantity,
					ProductId = stock.ProductId,
					CreatedOn = stock?.CreatedOn,
					ModifiedOn = DateTime.UtcNow
				});
			}

			await _stockManager.UpdateStockRange(stockList);

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
