﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart;
using Shop.Database;

namespace Shop.UI.Controllers
{
	[Route("[controller]/[action]")]
	public class CartController : Controller
	{
		private ApplicationDbContext _context;

		public CartController(ApplicationDbContext context) {
			_context = context;
		}

		[HttpPost("{stockId}")]
		public async Task<IActionResult> AddOne(Guid stockId) {
			var request = new AddToCart.Request {
				StockId = stockId,
				Quantity = 1
			};
			var addToCart = new AddToCart(HttpContext.Session, _context);

			var success = await addToCart.Do(request);
			if (success) {
				return Ok("Item added to cart");
			}
			return BadRequest("Failed to add to cart");
		}

		[HttpPost("{stockId}")]
		public async Task<IActionResult> RemoveOne(Guid stockId) {
			var request = new RemoveFromCart.Request {
				StockId = stockId,
				Quantity = 1
			};
			var removeFromCart = new RemoveFromCart(HttpContext.Session, _context);

			var success = await removeFromCart.Do(request);
			if (success) {
				return Ok("Item removed from cart");
			}
			return BadRequest("Failed to remove item from cart");
		}

		[HttpPost("{stockId}")]
		public async Task<IActionResult> RemoveAll(Guid stockId) {
			var request = new RemoveFromCart.Request {
				StockId = stockId,
				All = true
			};
			var removeFromCart = new RemoveFromCart(HttpContext.Session, _context);

			var success = await removeFromCart.Do(request);
			if (success) {
				return Ok("all items removed from cart");
			}
			return BadRequest("Failed to remove all items from cart");
		}
	}
}
