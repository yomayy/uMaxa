using Shop.Domain.Infrastructure;
using System;

namespace Shop.Application.ProductsAdmin
{
	[Service]
	public class GetProduct
	{
		private IProductManager _productManager;

		public GetProduct(IProductManager productManager) {
			_productManager = productManager;
		}

		public ProductViewModel Do(Guid id) =>
			_productManager.GetProductByIdWithCategory(id, p => new ProductViewModel {
				Id = p.Id,
				Name = p.Name,
				Description = p.Description,
				Value = p.Value,
				Category = new CategoryViewModel {
					Id = p?.Category?.Id,
					Name = p?.Category?.Name
				}
			});

		public class ProductViewModel
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
			public CategoryViewModel Category { get; set; }
		}

		public class CategoryViewModel
		{
			public Guid? Id { get; set; }
			public string Name { get; set; }
		}
	}
}
