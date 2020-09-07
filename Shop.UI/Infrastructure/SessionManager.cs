﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.UI.Infrastructure
{
	public class SessionManager : ISessionManager
	{
		private readonly ISession _session;

		public SessionManager(IHttpContextAccessor httpContextAccessor) {

			_session = httpContextAccessor.HttpContext.Session;
		}

		public void AddCustomerInformation(CustomerInformation customer) {
			var stringObj = JsonConvert.SerializeObject(customer);
			_session.SetString("customer-info", stringObj);
		}

		public void AddProduct(CartProduct cartProduct) {
			var cartList = new List<CartProduct>();
			var stringObj = _session.GetString("cart");
			if (!string.IsNullOrEmpty(stringObj)) {
				cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObj);
			}
			if (cartList.Any(x => x.StockId == cartProduct.StockId)) {
				cartList.Find(x => x.StockId == cartProduct.StockId).Quantity += cartProduct.Quantity;
			}
			else {
				cartList.Add(cartProduct);
			}

			stringObj = JsonConvert.SerializeObject(cartList);

			_session.SetString("cart", stringObj);
		}

		public IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector) {
			var stringObj = _session.GetString("cart");
			if (string.IsNullOrEmpty(stringObj)) {
				return new List<TResult>();
			}
			var cartList = JsonConvert.DeserializeObject<IEnumerable<CartProduct>>(stringObj);
			return cartList.Select(selector);
		}

		public CustomerInformation GetCustomerInformation() {
			var stringObj = _session.GetString("customer-info");
			if (string.IsNullOrEmpty(stringObj)) {
				return null;
			}
			var customerInformation = JsonConvert.DeserializeObject<CustomerInformation>(stringObj);
			return customerInformation;
		}

		public string GetId() => _session.Id;

		public void RemoveProduct(Guid stockId, int quantity) {
			var cartList = new List<CartProduct>();
			var stringObj = _session.GetString("cart");
			if (string.IsNullOrEmpty(stringObj)) {
				return;
			}
			cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObj);

			if (!cartList.Any(x => x.StockId == stockId)) {
				return;
			}
			var product = cartList.First(x => x.StockId == stockId);
			product.Quantity -= quantity;

			if(product.Quantity <= 0) {
				cartList.Remove(product);
			}

			stringObj = JsonConvert.SerializeObject(cartList);

			_session.SetString("cart", stringObj);
		}
	}
}
