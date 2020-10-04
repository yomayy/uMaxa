using Shop.Domain.Models;
using System;

namespace Shop.Domain.Infrastructure
{
	public interface IShopUserManager
	{
		TResult GetShopUser<TResult>(Func<ShopUser, TResult> selector, string email, string password = null);
		ShopUser CreateShopUser(string username, string password, string email);
	}
}
