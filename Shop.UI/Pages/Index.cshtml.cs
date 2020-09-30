using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.CategoriesAdmin;
using Shop.Application.Products;
using System.Collections.Generic;

namespace Shop.UI.Pages
{
	public class IndexModel : PageModel
	{
		public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }
		public IEnumerable<GetCategories.CategoryViewModel> Categories { get; set; }

		public string selectedCategoryId = "";

		public void OnGet(
				[FromServices] GetProducts getProducts,
				[FromServices] GetCategories getCategories) {
			Products = getProducts.Do();
			Categories = getCategories.Do();
		}

		public void OnGetByCategoryId(
				string categoryId,
				[FromServices] GetProducts getProducts,
				[FromServices] GetCategories getCategories) {
			Products = getProducts.Do(categoryId);
			selectedCategoryId = categoryId;
			Categories = getCategories.Do();
		}
	}
}
