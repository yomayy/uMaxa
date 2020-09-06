using Shop.Application.Infrastructure;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
	public class AddToCart
	{
		private ApplicationDbContext _context;
		private ISessionManager _sessionManager;

		public AddToCart(ISessionManager sessionManager, ApplicationDbContext context) {
			_context = context;
			_sessionManager = sessionManager;
		}

		public class Request
		{
			public Guid StockId { get; set; }
			public int Quantity { get; set; }
		}

		public async Task<bool> Do(Request request) {

			var stockOnHold = _context.StocksOnHold.Where(x => x.SessionId == _sessionManager.GetId())
				?.ToList();
			var stockToHold = _context.Stocks.Where(x => x.Id == request.StockId)
				.FirstOrDefault();

			if(stockToHold.Quantity < request.Quantity) {
				return false;
			}

			if(stockOnHold.Any(x => x.StockId == request.StockId)) {
				stockOnHold.Find(x => x.StockId == request.StockId).Quantity += request.Quantity;
			}
			else {
				_context.StocksOnHold.Add(new StockOnHold {
					StockId = stockToHold.Id,
					SessionId = _sessionManager.GetId(),
					Quantity = request.Quantity,
					ExpiryDate = DateTime.UtcNow.AddMinutes(20)
				});
			}

			stockToHold.Quantity = stockToHold.Quantity - request.Quantity;

			stockOnHold?.ForEach(stock => {
				stock.ExpiryDate = DateTime.UtcNow.AddMinutes(20);
			});

			await _context.SaveChangesAsync();

			_sessionManager.AddProduct(request.StockId, request.Quantity);

			return true;
		}
	}
}
