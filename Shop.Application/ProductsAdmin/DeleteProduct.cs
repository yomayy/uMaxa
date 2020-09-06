using Shop.Database;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
	public class DeleteProduct
	{
		private ApplicationDbContext _context;

		public DeleteProduct(ApplicationDbContext context) {
			_context = context;
		}

		public async Task<bool> Do(Guid id) {
			var Product = _context.Products
				.FirstOrDefault(x => x.Id == id);
			_context.Products.Remove(Product);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
