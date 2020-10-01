using Shop.Application.CategoriesAdmin;
using System.Collections.Generic;

namespace Shop.Application.Products.Paging
{
	[Service]
	public class IndexViewModel
	{
		public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }
		public IEnumerable<GetCategories.CategoryViewModel> Categories { get; set; }
	}
}
