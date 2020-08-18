using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.ProductsAdmin
{
	public class GetProduct
	{
		private ApplicationDbContext _context;

		public GetProduct(ApplicationDbContext context) {
			_context = context;
		}

		public ProductViewModel Do(Guid id) =>
			_context.Products.Where(x => x.Id == id).Select(p => new ProductViewModel {
				Id = p.Id,
				Name = p.Name,
				Description = p.Description,
				Value = p.Value
			})
			.FirstOrDefault();

		public class ProductViewModel
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
		}
	}
}
