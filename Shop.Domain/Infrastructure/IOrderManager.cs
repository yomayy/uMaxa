using Shop.Domain.Enums;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Domain.Infrastructure
{
	public interface IOrderManager
	{
		bool OrderReferenceExists(string reference);

		IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus status, Func<Order, TResult> selector);
		IEnumerable<TResult> GetOrderByEmail<TResult>(Func<Order, TResult> selector, string email);
		TResult GetOrderById<TResult>(Guid id, Func<Order, TResult> selector);
		TResult GetOrderByReference<TResult>(string reference, Func<Order, TResult> selector);

		Task<int> CreateOrder(Order order);
		Task<int> AdvanceOrder(Guid id);
	}
}
