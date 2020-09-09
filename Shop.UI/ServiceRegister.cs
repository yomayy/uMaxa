using Shop.Application;
using Shop.Database;
using Shop.Domain.Infrastructure;
using Shop.UI.Infrastructure;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceRegister
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection @this) {

			{
			/*
			#region Cart
			@this.AddTransient<AddCustomerInformation>();
			@this.AddTransient<AddToCart>();
			@this.AddTransient<GetCart>();
			@this.AddTransient<GetCustomerInformation>();
			@this.AddTransient<Shop.Application.Cart.GetOrder>();
			@this.AddTransient<RemoveFromCart>();
			#endregion

			#region Orders
			@this.AddTransient<CreateOrder>();
			@this.AddTransient<Shop.Application.Orders.GetOrder>();
			#endregion

			#region OrdersAdmin
			@this.AddTransient<Shop.Application.OrdersAdmin.GetOrder>();
			@this.AddTransient<GetOrders>();
			@this.AddTransient<UpdateOrder>();
			#endregion

			#region Products
			@this.AddTransient<Shop.Application.Products.GetProduct>();
			@this.AddTransient<Shop.Application.Products.GetProducts>();
			#endregion

			#region ProductsAdmin
			@this.AddTransient<CreateProduct>();
			@this.AddTransient<DeleteProduct>();
			@this.AddTransient<Shop.Application.ProductsAdmin.GetProduct>();
			@this.AddTransient<Shop.Application.ProductsAdmin.GetProducts>();
			@this.AddTransient<UpdateProduct>();
			#endregion

			#region StockAdmin
			@this.AddTransient<CreateStock>();
			@this.AddTransient<DeleteStock>();
			@this.AddTransient<GetStock>();
			@this.AddTransient<UpdateStock>();
			#endregion

			#region UsersAdmin
			@this.AddTransient<CreateUser>();
			#endregion
			*/
			}

			var serviceType = typeof(Service);
			var definedTypes = serviceType.Assembly.DefinedTypes;

			var services = definedTypes
				.Where(x => x.GetTypeInfo().GetCustomAttribute<Service>() != null);

			foreach (var service in services) {
				@this.AddTransient(service);
			}

			@this.AddTransient<IStockManager, StockManager>();
			@this.AddTransient<IProductManager, ProductManager>();
			@this.AddTransient<IOrderManager, OrderManager>();
			@this.AddScoped<ISessionManager, SessionManager>();

			return @this;
		}
	}
}
