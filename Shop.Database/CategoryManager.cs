using Microsoft.EntityFrameworkCore;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Database
{
	public class CategoryManager : ICategoryManager
	{
		private readonly ApplicationDbContext _context;

		public CategoryManager(ApplicationDbContext context) {
			_context = context;
		}

		public Task<int> CreateCategory(Category category) {
			_context.Categories.Add(category);
			return _context.SaveChangesAsync();
		}

		public Task<int> DeleteCategory(Guid id) {
			var category = _context.Categories.FirstOrDefault(x => x.Id == id);
			_context.Categories.Remove(category);
			return _context.SaveChangesAsync();
		}

		public IEnumerable<TResult> GetCategoriesWithProduct<TResult>(
				Func<Category, TResult> selector) {
			return _context.Categories
				.Include(x => x.Products)
				.Select(selector)
				.ToList();
		}

		public TResult GetCategoryById<TResult>(
				Guid id, 
				Func<Category, TResult> selector) {
			return _context.Categories
				.Where(x => x.Id == id)
				.Select(selector)
				.FirstOrDefault();
		}

		public TResult GetCategoryByName<TResult>(
				string name, 
				Func<Category, TResult> selector) {
			return _context.Categories
				.Include(x => x.Products)
				.Where(x => x.Name == name)
				.Select(selector)
				.FirstOrDefault();
		}

		public Task<int> UpdateCategory(Category category) {
			_context.Categories.Update(category);
			return _context.SaveChangesAsync();
		}
	}
}
