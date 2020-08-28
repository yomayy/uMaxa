using Microsoft.EntityFrameworkCore;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.Products
{
	public class GetProduct
	{
		private ApplicationDbContext _context;

		public GetProduct(ApplicationDbContext context) {
			_context = context;
		}

		public ProductViewModel Do(string name) {
			return _context.Products
				.Include(x => x.Stocks)
				.Where(x => x.Name == name)
				.Select(p => new ProductViewModel {
					Name = p.Name,
					Description = p.Description,
					Value = $"{p.Value.ToString("N2")} $", // 1100.50 => 1,100.50 => $ 1,100.50
					Stocks = p.Stocks.Select(y => new StockViewModel {
						Id = y.Id,
						Description = y.Description,
						InStock = y.Quantity > 0
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
			public bool InStock { get; set; }
		}
	}
}
