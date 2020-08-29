using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.Cart
{
	public class GetCart
	{
		private ISession _session;
		private ApplicationDbContext _context;

		public GetCart(ISession session, ApplicationDbContext context) {
			_session = session;
			_context = context;
		}

		public class Response
		{
			public string Name { get; set; }
			public string Value { get; set; }
			public int Quantity { get; set; }
			public Guid StockId { get; set; }
		}

		public IEnumerable<Response> Do() {
			//TODO: account for the multiple items in the cart.
			var stringObj = _session.GetString("cart");

			if (string.IsNullOrEmpty(stringObj)) {
				return new List<Response>();
			}
			var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObj);

			var response = _context.Stocks
				.Include(x => x.Product)
				.Where(x => cartList.Any(y => y.StockId == x.Id))
				.Select(x => new Response {
					Name = x.Product.Name,
					Value = $"{x.Product.Value.ToString("N2")} $",
					StockId = x.Id,
					Quantity = cartList.FirstOrDefault(y => y.StockId == x.Id).Quantity
				})
				.ToList();
			return response;
		}
	}
}
