using Microsoft.EntityFrameworkCore;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Linq;

namespace Shop.Database
{
	public class ShopUserManager : IShopUserManager
	{
		private ApplicationDbContext _context;

		private const string USER_ROLE = "user";

		public ShopUserManager(ApplicationDbContext context) {
			_context = context;
		}
		public TResult GetShopUser<TResult>(Func<ShopUser, TResult> selector,
				string email, string password = null) {
			if (!string.IsNullOrEmpty(password)) {
				var user = _context.ShopUsers
					.Where(u => u.Email == email && u.Password == password)
					.Include(u => u.Role)
					.Select(selector)
					.FirstOrDefault();
				return user;
			}
			else {
				var user = _context.ShopUsers
					.Where(u => u.Email == email)
					.Select(selector)
					.FirstOrDefault();
				return user;
			}
		}

		public ShopUser CreateShopUser(
				string username, string password, string email) {
			var user = new ShopUser { 
				Name = username, 
				Password = password,
				Email = email
			};
			Role userRole = _context.ShopRoles.FirstOrDefault(r => r.Name == USER_ROLE);
			if (userRole != null)
				user.Role = userRole;

			_context.ShopUsers.Add(user);
			_context.SaveChangesAsync();

			return user;
		}
	}
}
