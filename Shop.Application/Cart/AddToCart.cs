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
	public class AddToCart
	{
		private ApplicationDbContext _context;
		private ISession _session;

		public AddToCart(ISession session, ApplicationDbContext context) {
			_context = context;
			_session = session;
		}

		public class Request
		{
			public Guid StockId { get; set; }
			public int Quantity { get; set; }
		}

		public async Task<bool> Do(Request request) {



			var stockToHold = _context.Stocks.Where(x => x.Id == request.StockId)
				.FirstOrDefault();
			if(stockToHold.Quantity < request.Quantity) {
				return false;
			}

			_context.StocksOnHold.Add(new StockOnHold {
				StockId = stockToHold.Id,
				Quantity = request.Quantity,
				ExpiryDate = DateTime.UtcNow.AddMinutes(20)
			});

			stockToHold.Quantity = stockToHold.Quantity - request.Quantity;

			await _context.SaveChangesAsync();

			var cartList = new List<CartProduct>();
			var stringObj = _session.GetString("cart");
			if (!string.IsNullOrEmpty(stringObj)) {
				cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObj);
			}
			if(cartList.Any(x => x.StockId == request.StockId)) {
				cartList.Find(x => x.StockId == request.StockId).Quantity += request.Quantity;
			}
			else {
				cartList.Add(new CartProduct {
					StockId = request.StockId,
					Quantity = request.Quantity
				});
			}

			stringObj = JsonConvert.SerializeObject(cartList);

			_session.SetString("cart", stringObj);

			return true;
		}
	}
}
