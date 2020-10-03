using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.OrdersAdmin;
using Shop.UI.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
	[Route("[controller]")]
	[Authorize(Policy = "Manager")]
	public class OrdersController : Controller {
		[HttpGet("")]
		public IActionResult GetOrders(
			int status,
			[FromServices] GetOrders getOrders) =>
				Ok(getOrders.Do(status));

		[HttpGet("{id}")]
		public IActionResult GetOrder(
			Guid id,
			[FromServices] GetOrder getOrder) =>
				Ok(getOrder.Do(id));

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateOrder(
			Guid id,
			[FromServices] UpdateOrder updateOrder,
			[FromServices] EmailManager emailManager) {
			var success = await updateOrder.DoAsync(id) > 0;
			await emailManager.DoAsync(id);
			if (success) {
				return Ok();
			}
			else {
				return BadRequest();
			}
		}
	}
}
