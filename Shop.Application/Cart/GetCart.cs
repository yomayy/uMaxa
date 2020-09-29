using Shop.Domain.Infrastructure;
using System;
using System.Collections.Generic;

namespace Shop.Application.Cart
{
	[Service]
	public class GetCart
	{
		private ISessionManager _sessionManager;

		public GetCart(ISessionManager sessionManager) {
			_sessionManager = sessionManager;
		}

		public class Response
		{
			public string Name { get; set; }
			public string Value { get; set; }
			public string ProductImage { get; set; }
			public decimal RealValue { get; set; }
			public int Quantity { get; set; }
			public Guid StockId { get; set; }
			public string StockDescription { get; set; }
		}

		public IEnumerable<Response> Do() {
			//TODO: account for the multiple items in the cart.
			return _sessionManager
				.GetCart(x => new Response {
					Name = x.ProductName,
					ProductImage = x?.ProductImage,
					Value = x.Value.GetValueString(),
					RealValue = x.Value,
					StockId = x.StockId,
					StockDescription = x.StockDescription,
					Quantity = x.Quantity
				});
		}
	}
}
