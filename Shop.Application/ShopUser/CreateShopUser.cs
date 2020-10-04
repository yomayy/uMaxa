using Shop.Domain.Infrastructure;

namespace Shop.Application.ShopUser
{
	[Service]
	public class CreateShopUser
	{
		private readonly IShopUserManager _shopUserManager;
		public CreateShopUser(IShopUserManager shopUserManager) {
			_shopUserManager = shopUserManager;
		}

		public Response Do(string username, string password, string email) {
			var shopUser = _shopUserManager.CreateShopUser(username, password, email);
			Response response = new Response {
				Name = shopUser?.Name,
				RoleName = shopUser?.Role?.Name,
				Email = shopUser?.Email
			};
			return response;
		}

		public class Response
		{
			public string Name { get; set; }
			public string Email { get; set; }
			public string RoleName { get; set; }
		}
	}
}
