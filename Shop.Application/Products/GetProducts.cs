using Shop.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application.Products
{
	[Service]
	public class GetProducts
	{
		private IProductManager _productManager;

		public GetProducts(IProductManager productManager) {
			_productManager = productManager;
		}

		public IEnumerable<ProductViewModel> Do() {
			return _productManager.GetProductsWithStock(p => new ProductViewModel {
				Name = p.Name,
				Description = p.Description,
				Value = p.Value.GetValueString(),
				Image = p?.ProductImage,
				StockCount = p.Stocks.Sum(y => y.Quantity)
			});
		}

		public IEnumerable<ProductViewModel> Do(string searchString) {
			return _productManager.GetProductsWithStock(p => new ProductViewModel {
				Name = p.Name,
				Description = p.Description,
				Value = p.Value.GetValueString(),
				Image = p?.ProductImage,
				StockCount = p.Stocks.Sum(y => y.Quantity)
			}, searchString);
		}

		public IEnumerable<ProductViewModel> Do(string categoryId,
				int pageNumber, int pageSize) {
			Guid? cid = Guid.Parse(categoryId);
			return _productManager.GetProductByCategoryId(cid, p => new ProductViewModel {
				Name = p.Name,
				Description = p.Description,
				Value = p.Value.GetValueString(),
				Image = p?.ProductImage,
				StockCount = p.Stocks.Sum(y => y.Quantity)
			}, pageNumber, pageSize);
		}

		public IEnumerable<ProductViewModel> Do(
				int pageNumber, int pageSize) {
			return _productManager.GetProductsWithStock(p => new ProductViewModel {
				Name = p.Name,
				Description = p.Description,
				Value = p.Value.GetValueString(),
				Image = p?.ProductImage,
				StockCount = p.Stocks.Sum(y => y.Quantity)
			},
			pageNumber, pageSize);
		}

		public Task<int> GetProductsCount() {
			return _productManager.GetProductsCount();
		}

		public Task<int> GetProductsCount(string categoryId) {
			Guid? cid = Guid.Parse(categoryId);
			return _productManager.GetProductsCount(cid);
		}

		public class ProductViewModel
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public string Value { get; set; }
			public string Image { get; set; }
			public int StockCount { get; set; }
		}
	}
}
