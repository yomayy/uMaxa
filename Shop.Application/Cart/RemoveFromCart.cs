using Shop.Application.Infrastructure;
using Shop.Database;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
	public class RemoveFromCart
	{
		private ApplicationDbContext _context;
		private ISessionManager _sessionManager;

		public RemoveFromCart(ISessionManager sessionManager, ApplicationDbContext context) {
			_context = context;
			_sessionManager = sessionManager;
		}

		public class Request
		{
			public Guid StockId { get; set; }
			public int Quantity { get; set; }
			public bool All { get; set; }
		}

		public async Task<bool> Do(Request request) {
			var stockOnHold = _context.StocksOnHold
				.FirstOrDefault(x => x.StockId == request.StockId
				&& x.SessionId == _sessionManager.GetId());

			var stock = _context.Stocks.FirstOrDefault(x => x.Id == request.StockId);

			if (request.All) {
				stock.Quantity += stockOnHold.Quantity;
				_sessionManager.RemoveProduct(request.StockId, stockOnHold.Quantity);
				stockOnHold.Quantity = 0;
			}
			else {
				stock.Quantity += request.Quantity;
				stockOnHold.Quantity -= request.Quantity;
				_sessionManager.RemoveProduct(request.StockId, request.Quantity);
			}

			if(stockOnHold.Quantity <= 0) {
				_context.Remove(stockOnHold);
			}
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
