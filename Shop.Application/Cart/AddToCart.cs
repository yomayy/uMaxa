using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
	[Service]
	public class AddToCart
	{
		private ISessionManager _sessionManager;
		private IStockManager _stockManager;

		public AddToCart(
				ISessionManager sessionManager,
				IStockManager stockManager) {
			_sessionManager = sessionManager;
			_stockManager = stockManager;
		}

		public class Request
		{
			public Guid StockId { get; set; }
			public int Quantity { get; set; }
		}

		public async Task<bool> DoAsync(Request request) {
			// service responsibility
			if(!_stockManager.EnoughStock(request.StockId, request.Quantity)) {
				return false;
			}

			await _stockManager
				.PutStockOnHold(request.StockId, request.Quantity, _sessionManager.GetId());

			var stock = _stockManager.GetStockWithProduct(request.StockId);

			var cartProduct = new CartProduct() {
				ProductId = stock.ProductId,
				ProductName = stock.Product.Name,
				StockId = stock.Id,
				StockDescription = stock.Description,
				Quantity = request.Quantity,
				Value = stock.Product.Value
			};

			_sessionManager.AddProduct(cartProduct);

			return true;
		}
	}
}
