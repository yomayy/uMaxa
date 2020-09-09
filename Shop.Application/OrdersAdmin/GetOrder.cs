﻿using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.OrdersAdmin
{
	public class GetOrder
	{
		private IOrderManager _orderManager;

		public GetOrder(IOrderManager orderManager) {
			_orderManager = orderManager;
		}

		public class Response
		{
			public Guid Id { get; set; }
			public string OrderRef { get; set; }
			public string StripeReference { get; set; }

			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string Email { get; set; }
			public string PhoneNumber { get; set; }
			public string Address1 { get; set; }
			public string Address2 { get; set; }
			public string City { get; set; }
			public string PostCode { get; set; }

			public IEnumerable<Product> Products { get; set; }
		}

		public class Product
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public int Quantity { get; set; }
			public string StockDescription { get; set; }
		}

		public Response Do(Guid id) =>
			_orderManager.GetOrderById(id, Projection);

		private static Func<Order, Response> Projection = (order) =>
			new Response {
				Id = order.Id,
				OrderRef = order.OrderRef,
				StripeReference = order.StripeReference,

				FirstName = order.FirstName,
				LastName = order.LastName,
				Email = order.Email,
				PhoneNumber = order.PhoneNumber,
				Address1 = order.Address1,
				Address2 = order.Address2,
				City = order.City,
				PostCode = order.PostCode,

				Products = order.OrderStocks.Select(y => new Product {
					Name = y.Stock.Product.Name,
					Description = y.Stock.Product.Description,
					Quantity = y.Quantity,
					StockDescription = y.Stock.Description
				})
			};
	}
}
