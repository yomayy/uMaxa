using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Domain.Infrastructure
{
	public interface IStockManager
	{
		Task<int> CreateStock(Stock stock);
		Task<int> DeleteStock(Guid id);
		Task<int> UpdateStockRange(List<Stock> stockList);

		Stock GetStockWithProduct(Guid stockId);
		bool EnoughStock(Guid stockId, int quantity);
		Task PutStockOnHold(Guid stockId, int quantity, string sessionId);

		Task RemoveStockFromHold(Guid stockId, int quantity, string sessionId);
		Task RemoveStockFromHold(string sessionId);
		Task RetrieveExpiredStockOnHold();
	}
}
