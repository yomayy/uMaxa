﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.CategoriesAdmin;
using Shop.Application.Products;
using Shop.Application.Products.Paging;

namespace Shop.UI.Pages
{
	[BindProperties]
	public class IndexModel : PageModel
	{
		public IndexViewModel IndexViewModel { get; set; }
		public PageViewModel PageViewModel { get; set; }

		public string selectedCategoryId = "";

		private const int PageZize = 2;

		[BindProperty(SupportsGet = true)]
		public string SearchString { get; set; }

		public void OnGet(
				[FromServices] GetProducts getProducts,
				[FromServices] GetCategories getCategories,
				int pageNumber = 1, int pageSize = PageZize) {
			IndexViewModel = new IndexViewModel {
				Products = getProducts.Do(pageNumber, pageSize),
				Categories = getCategories.Do()
			};
			var taskCount = getProducts.GetProductsCount();
			int count = taskCount.Result;
			PageViewModel = new PageViewModel(count, pageNumber, PageZize);
		}

		public void OnGetByCategoryId(
				[FromServices] GetProducts getProducts,
				[FromServices] GetCategories getCategories,
				string categoryId,
				int pageNumber = 1, int pageSize = PageZize) {
			IndexViewModel = new IndexViewModel {
				Products = getProducts.Do(categoryId, pageNumber, pageSize),
				Categories = getCategories.Do()
			};
			selectedCategoryId = categoryId;
			var taskCount = getProducts.GetProductsCount(selectedCategoryId);
			int count = taskCount.Result;
			PageViewModel = new PageViewModel(count, pageNumber, PageZize);
		}

		public void OnGetSearchProduct(
				[FromServices] GetProducts getProducts,
				[FromServices] GetCategories getCategories,
				string searchString) {
			IndexViewModel = new IndexViewModel {
				Products = string.IsNullOrEmpty(searchString) 
					? getProducts.Do(1, PageZize)
					: getProducts.Do(searchString),
				Categories = getCategories.Do()
			};
			var taskCount = getProducts.GetProductsCount();
			int count = taskCount.Result;
			PageViewModel = new PageViewModel(count, 1, PageZize);
		}
	}
}
