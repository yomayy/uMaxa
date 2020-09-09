using Shop.Domain.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
	public class DeleteStock
	{
		private IStockManager _stockManager;

		public DeleteStock(IStockManager stockManager) {
			_stockManager = stockManager;
		}

		public Task<int> Do(Guid id) {
			return _stockManager.DeleteStock(id);
		}
	}
}
