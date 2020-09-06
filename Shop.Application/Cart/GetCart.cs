using Microsoft.EntityFrameworkCore;
using Shop.Application.Infrastructure;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.Cart
{
	public class GetCart
	{
		private ISessionManager _sessionManager;
		private ApplicationDbContext _context;

		public GetCart(ISessionManager sessionManager, ApplicationDbContext context) {
			_sessionManager = sessionManager;
			_context = context;
		}

		public class Response
		{
			public string Name { get; set; }
			public string Value { get; set; }
			public decimal RealValue { get; set; }
			public int Quantity { get; set; }
			public Guid StockId { get; set; }
		}

		public IEnumerable<Response> Do() {
			//TODO: account for the multiple items in the cart.
			var cartList = _sessionManager.GetCart();
			if(cartList == null) {
				return new List<Response>();
			}
			var response = _context.Stocks
				.Include(x => x.Product)
				.Where(x => cartList.Any(y => y.StockId == x.Id))
				.Select(x => new Response {
					Name = x.Product.Name,
					Value = $"{x.Product.Value.ToString("N2")} $",
					RealValue = x.Product.Value,
					StockId = x.Id,
					Quantity = cartList.FirstOrDefault(y => y.StockId == x.Id).Quantity
				})
				.ToList();
			return response;
		}
	}
}
