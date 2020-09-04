using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.OrdersAdmin
{
	public class UpdateOrder
	{
		private ApplicationDbContext _context;

		public UpdateOrder(ApplicationDbContext context) {
			_context = context;
		}

		public async Task<bool> DoAsync(Guid id) {
			var order = _context.Orders.FirstOrDefault(x => x.Id == id);

			order.Status = order.Status + 1;

			return await _context.SaveChangesAsync() > 0;
		}
	}
}
