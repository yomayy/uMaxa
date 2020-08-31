using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Orders
{
	public class CreateOrder
	{
		private ApplicationDbContext _context;

		public CreateOrder(ApplicationDbContext context) {
			_context = context;
		}

		public class Request
		{
			public string StripeReference { get; set; }

			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string Email { get; set; }
			public string PhoneNumber { get; set; }
			public string Address1 { get; set; }
			public string Address2 { get; set; }
			public string City { get; set; }
			public string PostCode { get; set; }

			public List<Stock> Stocks { get; set; }
		}

		public class Stock
		{
			public Guid StockId { get; set; }
			public int Quantity { get; set; }
		}

		public async Task<bool> Do(Request request) {
			var stocksToUpdate = _context.Stocks
				?.Where(x => request.Stocks.Any(y => y.StockId == x.Id))
				?.ToList();

			stocksToUpdate?.ForEach(stock => {
				stock.Quantity = stock.Quantity - 
					request.Stocks.FirstOrDefault(x => x.StockId == stock?.Id).Quantity;
			});

			var order = new Order {
				OrderRef = CreateOrderReference(),
				StripeReference = request.StripeReference,
				FirstName = request.FirstName,
				LastName = request.LastName,
				Email = request.Email,
				PhoneNumber = request.PhoneNumber,
				Address1 = request.Address1,
				Address2 = request.Address2,
				City = request.City,
				PostCode = request.PostCode,
				OrderStocks = request.Stocks.Select(x => new OrderStock {
					StockId = x.StockId,
					Quantity = x.Quantity
				}).ToList()
			};
			_context.Orders.Add(order);
			return await _context.SaveChangesAsync() > 0;
		}

		public string CreateOrderReference() {
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var stringChars = new char[12];
			var random = new Random();
			do {
				for (int i = 0; i < stringChars.Length; i++) {
					stringChars[i] = chars[random.Next(chars.Length)];
				}
			} while (_context.Orders.Any(x => x.OrderRef == new string(stringChars)));
			var finalString = new String(stringChars);
			return finalString;
		}
	}
}
