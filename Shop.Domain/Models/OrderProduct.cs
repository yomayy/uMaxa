using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models
{
	public class OrderProduct
	{
		public Guid ProductId { get; set; }
		public Product Product { get; set; }

		public Guid OrderId { get; set; }
		public Order Order { get; set; }

		public int Quantity { get; set; }
		public Guid StockId { get; set; }
		public Stock Stock { get; set; }
	}
}
