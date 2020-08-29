using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Models
{
	public class CartProduct
	{
		public Guid StockId { get; set; }
		public int Quantity { get; set; }
	}
}
