using Shop.Database;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
	public class DeleteStock
	{
		private ApplicationDbContext _context;

		public DeleteStock(ApplicationDbContext context) {
			_context = context;
		}

		public async Task<bool> Do(Guid id) {
			var stock = _context?.Stocks
				.FirstOrDefault(x => x.Id == id);

			_context?.Stocks.Remove(stock);
			await _context.SaveChangesAsync();

			return true;
		}
	}
}
