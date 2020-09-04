using Microsoft.EntityFrameworkCore;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products
{
	public class GetProduct
	{
		private ApplicationDbContext _context;

		public GetProduct(ApplicationDbContext context) {
			_context = context;
		}

		public async Task<ProductViewModel> Do(string name) {
			var stocksOnHold = _context.StocksOnHold
				.Where(x => x.ExpiryDate < DateTime.UtcNow)
				.ToList();
			if (stocksOnHold.Count > 0) {
				var stockToReturn = _context.Stocks
					.Where(x => stocksOnHold.Any(y => y.StockId == x.Id))
					.ToList();
				foreach (var stock in stockToReturn) {
					stock.Quantity = stock.Quantity + 
						stocksOnHold.FirstOrDefault(x => x.StockId == stock.Id).Quantity;
				}
				_context.StocksOnHold.RemoveRange(stocksOnHold);
				await _context.SaveChangesAsync();
			}

			return _context.Products
				.Include(x => x.Stocks)
				.Where(x => x.Name == name)
				.Select(p => new ProductViewModel {
					Name = p.Name,
					Description = p.Description,
					Value = $"{p.Value.ToString("N2")}$", // 1100.50 => 1,100.50 => $ 1,100.50
					Stocks = p.Stocks.Select(y => new StockViewModel {
						Id = y.Id,
						Description = y.Description,
						Quantity = y.Quantity
					})
				})
				.FirstOrDefault();
		}

		public class ProductViewModel
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public string Value { get; set; }
			public IEnumerable<StockViewModel> Stocks { get; set; }
		}

		public class StockViewModel
		{
			public Guid Id { get; set; }
			public string Description { get; set; }
			public int Quantity { get; set; }
		}
	}
}
