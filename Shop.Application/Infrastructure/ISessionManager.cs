﻿using Shop.Domain.Models;
using System;
using System.Collections.Generic;

namespace Shop.Application.Infrastructure
{
	public interface ISessionManager
	{
		string GetId();
		void AddProduct(Guid stockId, int quantity);
		void RemoveProduct(Guid stockId, int quantity);
		List<CartProduct> GetCart();

		void AddCustomerInformation(CustomerInformation customer);
		CustomerInformation GetCustomerInformation();
	}
}
