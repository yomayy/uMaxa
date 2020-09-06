using System;

namespace Shop.Domain.Models
{
	public class CartProduct
	{
		public Guid StockId { get; set; }
		public int Quantity { get; set; }
	}
}
