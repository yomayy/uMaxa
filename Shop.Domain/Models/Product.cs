using Shop.Domain.BaseModels;
using System;
using System.Collections.Generic;

namespace Shop.Domain.Models
{
	public class Product : DbBase
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Value { get; set; }
		public string ProductImage { get; set; }

		public ICollection<Stock> Stocks { get; set; }

		public Guid? CategoryId { get; set; }
		public virtual Category Category { get; set; }
	}
}
