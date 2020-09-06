using System;

namespace Shop.Domain.Models
{
	public class StockOnHold
	{
		public Guid Id { get; set; }

		public string SessionId { get; set; }

		public Guid StockId { get; set; }
		public Stock Stock { get; set; }

		public int Quantity { get; set; }
		public DateTime ExpiryDate { get; set; }
	}
}
