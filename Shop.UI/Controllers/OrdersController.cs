using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.OrdersAdmin;
using System;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
	[Route("[controller]")]
	[Authorize(Policy = "Manager")]
	public class OrdersController : Controller
	{

		//private ApplicationDbContext _context;

		//public OrdersController(ApplicationDbContext context) {
		//	_context = context;
		//}
		//[HttpGet("")]
		//public IActionResult GetOrders(int status) => Ok(new GetOrders(_context).Do(status));

		[HttpGet("")]
		public IActionResult GetOrders(
			int status,
			[FromServices] GetOrders getOrders) =>
				Ok(getOrders.Do(status));

		//[HttpGet("{id}")]
		//public IActionResult GetOrder(Guid id) => Ok(new GetOrder(_context).Do(id));

		[HttpGet("{id}")]
		public IActionResult GetOrder(
			Guid id,
			[FromServices] GetOrder getOrder) =>
				Ok(getOrder.Do(id));

		//[HttpPut("{id}")]
		//public async Task<IActionResult> UpdateOrder(Guid id) => Ok((await new UpdateOrder(_context).Do(id)));

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateOrder(
			Guid id,
			[FromServices] UpdateOrder updateOrder) {
			var success = await updateOrder.DoAsync(id) > 0;
			if (success) {
				return Ok();
			}
			else {
				return BadRequest();
			}
		}
	}
}
