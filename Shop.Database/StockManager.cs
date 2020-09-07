﻿using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
	public class StockManager : IStockManager
	{
		private ApplicationDbContext _context;

		public StockManager(ApplicationDbContext context) {
			_context = context;
		}

		public bool EnoughStock(Guid stockId, int quantity) {
			return _context.Stocks
				?.FirstOrDefault(x => x.Id == stockId)
				?.Quantity >= quantity;
		}

		public Stock GetStockWithProduct(Guid stockId) {
			return _context.Stocks
				.Include(x => x.Product)
				.FirstOrDefault(x => x.Id == stockId);
		}

		// database responsibility to update the stock
		public Task PutStockOnHold(Guid stockId, int quantity, string sessionId) {
			// begin transaction

			// update Stocks set Quantity = Quantity + {0} where Id = {1}
			_context.Stocks.FirstOrDefault(x => x.Id == stockId).Quantity -= quantity;

			var stockOnHold = _context.StocksOnHold
				.Where(x => x.SessionId == sessionId)
				?.ToList();
			// select count(*) from StocksOnHold where StockId = {0} and sessionId = {1}
			if (stockOnHold.Any(x => x.StockId == stockId)) {
				// update StocksOnHold set Quantity = Quantity + {0} 
				// where StockId = {1} and sessionId = {2}
				stockOnHold.Find(x => x.StockId == stockId).Quantity += quantity;
			}
			else {
				// insert into StocksOnHold (StockId, SessionId, Quantity, ExpiryDate)
				// values ({0}{1}{2}{3})
				_context.StocksOnHold.Add(new StockOnHold {
					StockId = stockId,
					SessionId = sessionId,
					Quantity = quantity,
					ExpiryDate = DateTime.UtcNow.AddMinutes(20)
				});
			}

			// update StocksOnHold set ExpiryDate = {0} where sessionId = {1}
			stockOnHold?.ForEach(stock => {
				stock.ExpiryDate = DateTime.UtcNow.AddMinutes(20);
			});

			// commit transaction
			return _context.SaveChangesAsync();
		}

		public Task RemoveStockFromHold(Guid stockId, int quantity, string sessionId) {
			var stockOnHold = _context.StocksOnHold
				.FirstOrDefault(x => x.StockId == stockId
				&& x.SessionId == sessionId);

			var stock = _context.Stocks.FirstOrDefault(x => x.Id == stockId);
			stock.Quantity += quantity;
			stockOnHold.Quantity -= quantity;

			if (stockOnHold.Quantity <= 0) {
				_context.Remove(stockOnHold);
			}
			return _context.SaveChangesAsync();
		}
	}
}
