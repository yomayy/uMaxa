using Shop.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.ShopUser
{
	[Service]
	public class GetUserOrders
	{
		private IOrderManager _orderManager;

		public GetUserOrders(IOrderManager orderManager) {
			_orderManager = orderManager;
		}

		public IEnumerable<Response> Do(string email) {
			var orders = _orderManager.GetOrderByEmail(o => new Response {
				OrderRef = o?.OrderRef,
				StripeReference = o?.StripeReference,
				LastName = o?.LastName,
				FirstName = o?.FirstName,
				PhoneNumber = o?.PhoneNumber,
				Address1 = o?.Address1,
				Address2 = o?.Address2,
				City = o?.City,
				PostCode = o?.PostCode,
				Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), o?.Status.ToString()),
				OrderStockViewModel = o?.OrderStocks.Select(x => new OrderStockViewModel {
					Stock = new StockViewModel {
						Description = x?.Stock?.Description,
						Quantity = x.Stock.Quantity,
						ProductViewModel = new ProductViewModel {
							Name = x?.Stock?.Product?.Name,
							Description = x?.Stock?.Product?.Description,
							Value = x.Stock.Product.Value,
							ProductImage = x?.Stock?.Product?.ProductImage
						}
					}
				})
			}, email);
			return orders;
		}

		public class Response
		{
			public string OrderRef { get; set; }
			public string StripeReference { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string PhoneNumber { get; set; }
			public string Address1 { get; set; }
			public string Address2 { get; set; }
			public string City { get; set; }
			public string PostCode { get; set; }

			public OrderStatus Status { get; set; }
			public IEnumerable<OrderStockViewModel> OrderStockViewModel { get; set; }

		}
		
		public class OrderStockViewModel
		{
			public StockViewModel Stock { get; set; }

		}

		public enum OrderStatus
		{
			Pending,
			Packed,
			Shipped
		}

		public class StockViewModel
		{
			public string Description { get; set; }
			public int Quantity { get; set; }
			public ProductViewModel ProductViewModel { get; set; }
		}

		public class ProductViewModel
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
			public string ProductImage { get; set; }
		}
	}
}
