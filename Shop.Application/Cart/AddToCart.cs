﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.Cart
{
	public class AddToCart
	{
		private ISession _session;

		public AddToCart(ISession session) {
			_session = session;
		}

		public class Request
		{
			public Guid StockId { get; set; }
			public int Quantity { get; set; }
		}

		public void Do(Request request) {
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
		}
	}
}
