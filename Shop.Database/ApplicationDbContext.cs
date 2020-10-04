using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Models;
using System;

namespace Shop.Database
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) { }

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Stock> Stocks { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderStock> OrderStocks { get; set; }
		public DbSet<StockOnHold> StocksOnHold { get; set; }
		public DbSet<ShopUser> ShopUsers { get; set; }
		public DbSet<Role> ShopRoles { get; set; }

		protected override void OnModelCreating(ModelBuilder builder) {
			base.OnModelCreating(builder);
			builder.Entity<OrderStock>()
				.HasKey(x => new { x.StockId, x.OrderId });

			builder.Entity<Product>()
				.HasOne(p => p.Category)
				.WithMany(c => c.Products)
				.OnDelete(DeleteBehavior.SetNull);

			builder.Entity<ShopUser>()
				.HasOne(u => u.Role)
				.WithMany(r => r.ShopUsers)
				.OnDelete(DeleteBehavior.SetNull);

			string userRoleName = "user";
			Role userRole = new Role { Id = Guid.Parse("858ced3d-07e3-4fa7-a69e-1ca69955fd10"), Name = userRoleName };
			builder.Entity<Role>().HasData(new Role[] { userRole });
		}
	}
}
