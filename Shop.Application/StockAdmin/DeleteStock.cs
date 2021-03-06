﻿using Shop.Domain.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
	[Service]
	public class DeleteStock
	{
		private IStockManager _stockManager;

		public DeleteStock(IStockManager stockManager) {
			_stockManager = stockManager;
		}

		public Task<int> DoAsync(Guid id) {
			return _stockManager.DeleteStock(id);
		}
	}
}
