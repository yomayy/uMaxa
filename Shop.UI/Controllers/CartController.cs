using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart;
using System;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
	[Route("[controller]/[action]")]
	public class CartController : Controller
	{
		[HttpPost("{stockId}")]
		public async Task<IActionResult> AddOne(
			Guid stockId,
			[FromServices] AddToCart addToCart) {
			var request = new AddToCart.Request {
				StockId = stockId,
				Quantity = 1
			};

			var success = await addToCart.Do(request);
			if (success) {
				return Ok("Item added to cart");
			}
			return BadRequest("Failed to add to cart");
		}

		[HttpPost("{stockId}")]
		public async Task<IActionResult> RemoveOne(
			Guid stockId,
			[FromServices] RemoveFromCart removeFromCart) {
			var request = new RemoveFromCart.Request {
				StockId = stockId,
				Quantity = 1
			};

			var success = await removeFromCart.Do(request);
			if (success) {
				return Ok("Item removed from cart");
			}
			return BadRequest("Failed to remove item from cart");
		}

		[HttpPost("{stockId}")]
		public async Task<IActionResult> RemoveAll(
			Guid stockId,
			[FromServices] RemoveFromCart removeFromCart) {
			var request = new RemoveFromCart.Request {
				StockId = stockId,
				All = true
			};

			var success = await removeFromCart.Do(request);
			if (success) {
				return Ok("all items removed from cart");
			}
			return BadRequest("Failed to remove all items from cart");
		}
	}
}
