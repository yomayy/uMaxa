using System;

namespace Shop.Domain.Models
{
	public class CartProduct
	{
		public Guid ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductImage { get; set; }
		public Guid StockId { get; set; }
		public string StockDescription { get; set; }
		public int Quantity { get; set; }
		public decimal Value { get; set; }
	}
}
