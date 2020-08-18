using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.ProductsAdmin
{
	public class GetProducts
	{
		private ApplicationDbContext _context;

		public GetProducts(ApplicationDbContext context) {
			_context = context;
		}

		public IEnumerable<ProductViewModel> Do() {
			return _context.Products.ToList().Select(p => new ProductViewModel {
				Id = p.Id,
				Name = p.Name,
				Description = p.Description,
				Value = p.Value
			});
		}

		public class ProductViewModel
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
		}
	}
}
