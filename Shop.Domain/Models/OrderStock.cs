using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models
{
	public class OrderStock
	{
		public Guid OrderId { get; set; }
		public Order Order { get; set; }

		public Guid StockId { get; set; }
		public Stock Stock { get; set; }

		public int Quantity { get; set; }
	}
}
