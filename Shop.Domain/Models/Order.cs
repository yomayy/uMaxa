using Shop.Domain.BaseModels;
using Shop.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Shop.Domain.Models
{
	public class Order : DbBase
	{
		public string OrderRef { get; set; }
		public string StripeReference { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string PostCode { get; set; }

		public OrderStatus Status { get; set; }

		public ICollection<OrderStock> OrderStocks { get; set; }

		public Guid? ShopUserId { get; set; }
		public ShopUser ShopUser { get; set; }
	}
}
