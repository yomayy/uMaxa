﻿using Microsoft.EntityFrameworkCore;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.Products
{
	public class GetProducts
	{
		private ApplicationDbContext _context;

		public GetProducts(ApplicationDbContext context) {
			_context = context;
		}

		public IEnumerable<ProductViewModel> Do() {
			return _context.Products
				.Include(x => x.Stocks)
				.Select(p => new ProductViewModel {
					Name = p.Name,
					Description = p.Description,
					Value = $"{p.Value.ToString("N2")}$", // 1100.50 => 1,100.50 => $ 1,100.50

					StockCount = p.Stocks.Sum(y => y.Quantity)
				})
				.ToList();
		}

		public class ProductViewModel
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public string Value { get; set; }
			public int StockCount { get; set; }
		}
	}
}
