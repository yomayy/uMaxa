using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Application.Infrastructure;
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

		public void AddProduct(Guid stockId, int quantity) {
			var cartList = new List<CartProduct>();
			var stringObj = _session.GetString("cart");
			if (!string.IsNullOrEmpty(stringObj)) {
				cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObj);
			}
			if (cartList.Any(x => x.StockId == stockId)) {
				cartList.Find(x => x.StockId == stockId).Quantity += quantity;
			}
			else {
				cartList.Add(new CartProduct {
					StockId = stockId,
					Quantity = quantity
				});
			}

			stringObj = JsonConvert.SerializeObject(cartList);

			_session.SetString("cart", stringObj);
		}

		public List<CartProduct> GetCart() {
			var stringObj = _session.GetString("cart");
			if (string.IsNullOrEmpty(stringObj)) {
				return null;
			}
			var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObj);
			return cartList;
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
