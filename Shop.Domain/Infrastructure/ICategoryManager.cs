﻿using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Domain.Infrastructure
{
	public interface ICategoryManager
	{
		Task<int> CreateCategory(Category category);
		Task<int> DeleteCategory(Guid id);
		Task<int> UpdateCategory(Category category);

		TResult GetCategoryById<TResult>(Guid id, Func<Category, TResult> selector);
		TResult GetCategoryByName<TResult>(string name, Func<Category, TResult> selector);
		IEnumerable<TResult> GetCategoriesWithProduct<TResult>(Func<Category, TResult> selector);
	}
}
