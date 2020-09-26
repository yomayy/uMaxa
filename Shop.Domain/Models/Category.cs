using Shop.Domain.BaseModels;
using System.Collections.Generic;

namespace Shop.Domain.Models
{
	public class Category : DbBase
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public ICollection<Product> Products { get; set; }
	}
}
