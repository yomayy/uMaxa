﻿using Shop.Domain.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
	[Service]
	public class DeleteProduct
	{
		private IProductManager _productManager;

		public DeleteProduct(IProductManager productManager) {
			_productManager = productManager;
		}

		public Task<int> DoAsync(Guid id) {
			return _productManager.DeleteProduct(id);
		}
	}
}
