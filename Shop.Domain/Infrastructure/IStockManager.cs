using Shop.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
	public interface IStockManager
	{
		Stock GetStockWithProduct(Guid stockId);
		bool EnoughStock(Guid stockId, int quantity);
		Task PutStockOnHold(Guid stockId, int quantity, string sessionId);
		Task RemoveStockFromHold(Guid stockId, int quantity, string sessionId);
	}
}
