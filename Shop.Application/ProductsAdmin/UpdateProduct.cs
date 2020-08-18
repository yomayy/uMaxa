﻿using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
	public class UpdateProduct
	{
		private ApplicationDbContext _context;

		public UpdateProduct(ApplicationDbContext context) {
			_context = context;
		}

		public async Task<Response> Do(Request request) {
			var product = _context.Products
				.FirstOrDefault(x => x.Id == request.Id);

			product.Name = request.Name;
			product.Description = request.Description;
			product.Value = request.Value;
			product.ModifiedOn = DateTime.UtcNow;

			await _context.SaveChangesAsync();
			return new Response {
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Value = product.Value,
				ModifiedOn = product.ModifiedOn
			};
		}
		public class Request
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
		}

		public class Response
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public decimal Value { get; set; }
			public DateTime? ModifiedOn { get; set; }
		}
	}
}
