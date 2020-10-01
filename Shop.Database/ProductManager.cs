using Microsoft.EntityFrameworkCore;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Database
{
	public class ProductManager : IProductManager
	{
		private ApplicationDbContext _context;

		public ProductManager(ApplicationDbContext context) {
			_context = context;
		}

		public Task<int> CreateProduct(Product product) {
			_context.Products.Add(product);
			return _context.SaveChangesAsync();
		}

		public Task<int> DeleteProduct(Guid id) {
			var product = _context.Products.FirstOrDefault(x => x.Id == id);
			_context.Products.Remove(product);
			return _context.SaveChangesAsync();
		}

		public Task<int> UpdateProduct(Product product) {
			_context.Products.Update(product);
			return _context.SaveChangesAsync();
		}

		public TResult GetProductByIdWithCategory<TResult>(Guid id, Func<Product, TResult> selector) {
			return _context.Products
				.Where(x => x.Id == id)
				.Include(x => x.Category)
				.Select(selector)
				.FirstOrDefault();
		}
		public TResult GetProductById<TResult>(Guid id, Func<Product, TResult> selector) {
			return _context.Products
				.Where(x => x.Id == id)
				.Select(selector)
				.FirstOrDefault();
		}

		public TResult GetProductByName<TResult>(
				string name, 
				Func<Product, TResult> selector) {
			return _context.Products
				.Include(x => x.Stocks)
				.Where(x => x.Name == name)
				.Select(selector)
				.FirstOrDefault();
		}

		public IEnumerable<TResult> GetProductsWithStock<TResult>(
				Func<Product, TResult> selector) {
			var products = _context?.Products
				?.Include(x => x.Stocks)
				?.Include(x => x.Category)
				.Select(selector)
				.ToList();
			return products;
		}

		public IEnumerable<TResult> GetProductsWithStock<TResult>(
				Func<Product, TResult> selector,
				int pageNumber, int pageSize) {
			int excludeRecords = (pageSize * pageNumber) - pageSize;
			var products = _context?.Products
				?.Include(x => x.Stocks)
				?.Include(x => x.Category)
				?.Skip(excludeRecords)
				?.Take(pageSize)
				.Select(selector)
				.ToList();
			return products;
		}

		public IEnumerable<TResult> GetProductByCategoryId<TResult>(
				Guid? categoryId, 
				Func<Product, TResult> selector,
				int pageNumber, int pageSize) {
			int excludeRecords = (pageSize * pageNumber) - pageSize;
			var products = _context?.Products
				?.Where(p => p.CategoryId == categoryId)
				?.Include(x => x.Stocks)
				?.Include(x => x.Category)
				?.Skip(excludeRecords)
				?.Take(pageSize)
				.Select(selector)
				.ToList();
			return products;
		}

		public IEnumerable<TResult> GetProductByCategoryId<TResult>(
				Guid? categoryId,
				Func<Product, TResult> selector) {
			var products = _context?.Products
				?.Where(p => p.CategoryId == categoryId)
				?.Include(x => x.Stocks)
				?.Include(x => x.Category)
				.Select(selector)
				.ToList();
			return products;
		}

		public Task<int> GetProductsCount() {
			return _context.Products.CountAsync();
		}

		public Task<int> GetProductsCount(Guid? categoryId) {
			return _context.Products
				?.Where(p => p.CategoryId == categoryId)
				?.CountAsync();
		}
	}
}
