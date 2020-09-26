using Shop.Domain.BaseModels;
using Shop.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using Shop.Domain.Models;
using System.Linq;

namespace Shop.Application.CategoriesAdmin
{
	[Service]
	public class GetCategory
	{
		private readonly ICategoryManager _categoryManager;

		public GetCategory(ICategoryManager categoryManager) {
			_categoryManager = categoryManager;
		}

		public CategoryViewModel Do(Guid id) =>
			_categoryManager.GetCategoryById(id, c => new CategoryViewModel {
				Id = c.Id,
				Name = c.Name,
				Description = c?.Description,
				Products = c?.Products
			});

		public class ProductViewModel : DbBase
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public Guid? CategoryId { get; set; }
		}

		public class CategoryViewModel
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public IEnumerable<Product> Products { get; set; }
		}
	}
}
