﻿using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application.Orders
{
	[Service]
	public class CreateOrder
	{
		private IOrderManager _orderManager;
		private IStockManager _stockManager;

		public CreateOrder(
				IOrderManager orderManager,
				IStockManager stockManager) {
			_orderManager = orderManager;
			_stockManager = stockManager;
		}

		public class Request
		{
			public string StripeReference { get; set; }
			public string SessionId { get; set; }

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

		public async Task<bool> DoAsync(Request request) {
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

			var success = await _orderManager.CreateOrder(order) > 0;

			if (success) {
				await _stockManager.RemoveStockFromHold(request.SessionId);
				return true;
			}
			return false;
		}

		private string CreateOrderReference() {
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var stringChars = new char[12];
			var random = new Random();
			do {
				for (int i = 0; i < stringChars.Length; i++) {
					stringChars[i] = chars[random.Next(chars.Length)];
				}
			} while (_orderManager.OrderReferenceExists(new string(stringChars)));
			var finalString = new string(stringChars);
			return finalString;
		}
	}
}
