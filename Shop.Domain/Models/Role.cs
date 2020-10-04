using Shop.Domain.BaseModels;
using System.Collections.Generic;

namespace Shop.Domain.Models
{
	public class Role : DbBase
	{
		public string Name { get; set; }
		public List<ShopUser> ShopUsers { get; set; }
		public Role() {
			ShopUsers = new List<ShopUser>();
		}
	}
}
