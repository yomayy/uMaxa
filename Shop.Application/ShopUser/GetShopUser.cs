using Shop.Domain.BaseModels;
using Shop.Domain.Infrastructure;

namespace Shop.Application.ShopUser
{
	[Service]
	public class GetShopUser
	{
		private readonly IShopUserManager _shopUserManager;
		public GetShopUser(IShopUserManager shopUserManager) {
			_shopUserManager = shopUserManager;
		}

		public ShopUserViewModel Do(string email, string password = null) {
			return _shopUserManager.GetShopUser(x => new ShopUserViewModel {
				Id = x.Id,
				CreatedOn = x.CreatedOn,
				ModifiedOn = x.ModifiedOn,
				Name = x.Name,
				Email = x.Email,
				Role = new RoleViewModel {
					Name = x?.Role?.Name
				},
				Password = x.Password
			}, email, password);
		}

		public class ShopUserViewModel : DbBase
		{
			public string Name { get; set; }
			public string Email { get; set; }
			public string Password { get; set; }
			public RoleViewModel Role { get; set; }
		}

		public class RoleViewModel
		{
			public string Name { get; set; }
		}
	}
}
