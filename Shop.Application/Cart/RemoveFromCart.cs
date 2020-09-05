using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
	public class RemoveFromCart
	{
		private ApplicationDbContext _context;
		private ISession _session;

		public RemoveFromCart(ISession session, ApplicationDbContext context) {
			_context = context;
			_session = session;
		}

		public class Request
		{
			public Guid StockId { get; set; }
			public int Quantity { get; set; }
			public bool All { get; set; }
		}

		public async Task<bool> Do(Request request) {

			var cartList = new List<CartProduct>();
			var stringObj = _session.GetString("cart");
			if (string.IsNullOrEmpty(stringObj)) {
				return true;
			}
			cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObj);

			if(!cartList.Any(x => x.StockId == request.StockId)) {
				return true;
			}
			cartList.Find(x => x.StockId == request.StockId).Quantity -= request.Quantity;

			stringObj = JsonConvert.SerializeObject(cartList);

			_session.SetString("cart", stringObj);

			var stockOnHold = _context.StocksOnHold
				.FirstOrDefault(x => x.StockId == request.StockId
				&& x.SessionId == _session.Id);

			var stock = _context.Stocks.FirstOrDefault(x => x.Id == request.StockId);

			if (request.All) {
				stock.Quantity += stockOnHold.Quantity;
				stockOnHold.Quantity = 0;
			}
			else {
				stock.Quantity += request.Quantity;
				stockOnHold.Quantity -= request.Quantity;
			}

			if(stockOnHold.Quantity <= 0) {
				_context.Remove(stockOnHold);
			}
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
