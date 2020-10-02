﻿using Shop.Domain.BaseModels;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.CategoriesAdmin
{
	[Service]
	public class GetCategories
	{
		private ICategoryManager _categoryManager;

		public GetCategories(ICategoryManager categoryManager) {
			_categoryManager = categoryManager;
		}

		public IEnumerable<CategoryViewModel> Do() {
			return _categoryManager.GetCategoriesWithProduct(c => new CategoryViewModel {
				Id = c.Id,
				Name = c.Name,
				Description = c?.Description,
				Products = c?.Products
					.Select(y => new ProductViewModel {
						Id = y.Id,
						Name = y.Name,
						Description = y?.Description,
						Value = y.Value,
						Image = y?.ProductImage,
						CategoryId = c.Id
					})
			});
		}

		public IEnumerable<CategoryViewModel> Do(
				int pageNumber, int pageSize) {
			return _categoryManager.GetCategoriesWithProductPaging(c => new CategoryViewModel {
				Id = c.Id,
				Name = c.Name,
				Description = c?.Description,
				Products = c?.Products
					.Select(y => new ProductViewModel {
						Id = y.Id,
						Name = y.Name,
						Description = y?.Description,
						Value = y.Value,
						Image = y?.ProductImage,
						CategoryId = c.Id
					})
			}, pageNumber, pageSize);
		}

		public class ProductViewModel : DbBase
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
			public string Image { get; set; }
			public Guid? CategoryId { get; set; }
		}

		public class CategoryViewModel
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public IEnumerable<ProductViewModel> Products { get; set; }
		}
	}
}
