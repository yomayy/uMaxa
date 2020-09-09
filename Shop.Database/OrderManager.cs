using Microsoft.EntityFrameworkCore;
using Shop.Domain.Enums;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Database
{
	public class OrderManager : IOrderManager
	{
		private readonly ApplicationDbContext _context;

		public OrderManager(ApplicationDbContext context) {
			_context = context;
		}

		public bool OrderReferenceExists(string reference) {
			return _context.Orders.Any(x => x.OrderRef == reference);
		}

		public IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus status, Func<Order, TResult> selector) {
			return _context.Orders
				.Where(x => x.Status == status)
				.Select(selector)
				?.ToList();
		}

		private TResult GetOrder<TResult>(
				Func<Order, bool> condition,
				Func<Order, TResult> selector) {
			return _context.Orders
				.Where(x => condition(x))
				.Include(x => x.OrderStocks)
					.ThenInclude(x => x.Stock)
						.ThenInclude(x => x.Product)
				.Select(selector)
				.FirstOrDefault();
		}

		public TResult GetOrderById<TResult>(Guid id, Func<Order, TResult> selector) {
			return GetOrder(
				order => order.Id == id,
				selector);
		}

		public TResult GetOrderByReference<TResult>(
				string reference,
				Func<Order, TResult> selector) {
			return GetOrder(
				order => order.OrderRef == reference,
				selector);
		}

		public Task<int> CreateOrder(Order order) {
			_context.Orders.Add(order);
			return _context.SaveChangesAsync();
		}

		public Task<int> AdvanceOrder(Guid id) {
			var order = _context.Orders.FirstOrDefault(x => x.Id == id);
			order.Status++;
			order.ModifiedOn = DateTime.UtcNow;
			return _context.SaveChangesAsync();
		}
	}
}
