using Shop.Domain.BaseModels;
using System;
using System.Collections.Generic;

namespace Shop.Domain.Models
{
	public class ShopUser : DbBase
	{
		public string Name { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }

		public ICollection<Order> Orders { get; set; }

		public Guid? RoleId { get; set; }
		public Role Role { get; set; }
	}
}
