﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Products;
using Shop.Database;
using System.Collections.Generic;

namespace Shop.UI.Pages
{
	public class IndexModel : PageModel
	{
		private ApplicationDbContext _context;

		public IndexModel(ApplicationDbContext context) {
			_context = context;
		}

		public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }

		public void OnGet() {
			Products = new GetProducts(_context).Do();
		}
	}
}
