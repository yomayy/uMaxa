using Microsoft.EntityFrameworkCore;
using Shop.Application.Infrastructure;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.Cart
{
	public class GetOrder
	{
		private ISessionManager _sessionManager;
		private ApplicationDbContext _context;

		public GetOrder(ISessionManager sessionManager, ApplicationDbContext context) {
			_sessionManager = sessionManager;
			_context = context;
		}

		public class Response
		{
			public IEnumerable<Product> Products { get; set; }
			public CustomerInformation CustomerInformation { get; set; }

			public int GetTotalCharge() => Products.Sum(x => x.Value * x.Quantity);
		}

		public class Product
		{
			public Guid ProductId { get; set; }
			public int Quantity { get; set; }
			public Guid StockId { get; set; }
			public int Value { get; set; }
		}

		public class CustomerInformation
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string Email { get; set; }
			public string PhoneNumber { get; set; }
			public string Address1 { get; set; }
			public string Address2 { get; set; }
			public string City { get; set; }
			public string PostCode { get; set; }
		}

		public Response Do() {
			//TODO: account for the multiple items in the cart.

			var cart = _sessionManager.GetCart();

			var listOfProducts = _context?.Stocks
				.Include(x => x.Product)
				.Where(x => cart.Any(y => y.StockId == x.Id))
				.Select(x => new Product {
					ProductId = x.ProductId,
					StockId = x.Id,
					Value = (int) (x.Product.Value * 100),
					Quantity = cart.FirstOrDefault(y => y.StockId == x.Id).Quantity
				}).ToList();

			var customerInformation = _sessionManager.GetCustomerInformation();

			return new Response {
				Products = listOfProducts,
				CustomerInformation = new CustomerInformation {
					FirstName = customerInformation?.FirstName,
					LastName = customerInformation?.LastName,
					Email = customerInformation?.Email,
					PhoneNumber = customerInformation?.PhoneNumber,
					Address1 = customerInformation?.Address1,
					Address2 = customerInformation?.Address2,
					City = customerInformation?.City,
					PostCode = customerInformation?.PostCode
				}
			};
		}
	}
}
