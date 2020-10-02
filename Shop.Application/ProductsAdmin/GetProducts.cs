using Shop.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
	[Service]
	public class GetProducts
	{
		private IProductManager _productManager;

		public GetProducts(IProductManager productManager) {
			_productManager = productManager;
		}

		public IEnumerable<ProductViewModel> Do(
				int pageNumber, int pageSize) {
			var products = _productManager.GetProductsWithStock(p => new ProductViewModel {
				Id = p.Id,
				Name = p.Name,
				Description = p.Description,
				Value = p.Value,
				Image = p?.ProductImage,
				StockCount = p.Stocks.Sum(y => y.Quantity),
				Category = new CategoryViewModel {
					Id = p?.Category?.Id,
					Name = p?.Category?.Name
				}
			}, pageNumber, pageSize);
			return products;
		}

		public Task<int> GetProductsCount() {
			return _productManager.GetProductsCount();
		}
		
		public Task<int> GetProductsCount(Guid? categoryId) {
			return _productManager.GetProductsCount(categoryId);
		}

		public class ProductViewModel
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
			public string Image { get; set; }
			public int StockCount { get; set; }
			public CategoryViewModel Category { get; set; } = null;
		}

		public class CategoryViewModel
		{
			public Guid? Id { get; set; }
			public string Name { get; set; } //= null;
		}
	}
}
