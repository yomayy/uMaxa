﻿using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart;
using System;
using System.Linq;
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

			var success = await addToCart.DoAsync(request);
			if (success) {
				return Ok("Item added to cart");
			}
			return BadRequest("Failed to add to cart");
		}

		[HttpPost("{stockId}/{quantity}")]
		public async Task<IActionResult> Remove(
			Guid stockId,
			int quantity,
			[FromServices] RemoveFromCart removeFromCart) {
			var request = new RemoveFromCart.Request {
				StockId = stockId,
				Quantity = quantity
			};

			var success = await removeFromCart.DoAsync(request);
			if (success) {
				return Ok("Item removed from cart");
			}
			return BadRequest("Failed to remove item from cart");
		}

		[HttpGet]
		public IActionResult GetCartComponent([FromServices] GetCart getCart) {
			var totalValue = getCart.Do()
					?.Sum(x => x?.RealValue * x?.Quantity);
			return PartialView("Components/Cart/Small", $"{totalValue}$");
		}

		[HttpGet]
		public IActionResult GetCartMain([FromServices] GetCart getCart) {
			var cart = getCart.Do();
			return PartialView("_CartPartial", cart);
		}
	}
}
