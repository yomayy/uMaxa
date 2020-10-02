using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Domain.Infrastructure
{
	public interface IProductManager
	{
		Task<int> CreateProduct(Product product);
		Task<int> DeleteProduct(Guid id);
		Task<int> UpdateProduct(Product product);

		TResult GetProductByIdWithCategory<TResult>(Guid id, Func<Product, TResult> selector);
		TResult GetProductById<TResult>(Guid id, Func<Product, TResult> selector);
		IEnumerable<TResult> GetProductByCategoryId<TResult>(Guid? categoryId, Func<Product, TResult> selector, int pageNumber, int pageSize);
		IEnumerable<TResult> GetProductByCategoryId<TResult>(Guid? categoryId, Func<Product, TResult> selector);
		TResult GetProductByName<TResult>(string name, Func<Product, TResult> selector);
		IEnumerable<TResult> GetProductsWithStock<TResult>(Func<Product, TResult> selector);
		IEnumerable<TResult> GetProductsWithStock<TResult>(Func<Product, TResult> selector, int pageNumber = 1, int pageSize = 2);
		IEnumerable<TResult> GetProductsWithStock<TResult>(Func<Product, TResult> selector, string searchString);
		Task<int> GetProductsCount();
		Task<int> GetProductsCount(Guid? categoryId);
	}
}
