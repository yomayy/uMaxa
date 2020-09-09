using Shop.Domain.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Shop.Application.OrdersAdmin
{
	public class UpdateOrder
	{
		private IOrderManager _orderManager;

		public UpdateOrder(IOrderManager orderManager) {
			_orderManager = orderManager;
		}

		public Task<int> DoAsync(Guid id) {
			return _orderManager.AdvanceOrder(id);
		}
	}
}
