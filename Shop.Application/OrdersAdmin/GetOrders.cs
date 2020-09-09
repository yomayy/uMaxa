using Shop.Domain.Enums;
using Shop.Domain.Infrastructure;
using System;
using System.Collections.Generic;

namespace Shop.Application.OrdersAdmin
{
	public class GetOrders {

		private readonly IOrderManager _orderManager;

		public GetOrders(IOrderManager orderManager) {
			_orderManager = orderManager;
		}

		public class Response {
			public Guid Id { get; set; }
			public string OrderRef { get; set; }
			public string Email { get; set; }
		}

		public IEnumerable<Response> Do(int status) =>
			_orderManager.GetOrdersByStatus((OrderStatus)status,
				x => new Response {
					Id = x.Id,
					OrderRef = x.OrderRef,
					Email = x.Email
				});
	}
}
