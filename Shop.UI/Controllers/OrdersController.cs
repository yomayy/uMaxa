using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
	[Route("[controller]")]
	[Authorize(Policy = "Manager")]
	public class OrdersController : Controller
	{

		private ApplicationDbContext _context;

		public OrdersController(ApplicationDbContext context) {
			_context = context;
		}
		//[HttpGet("orders")]
		//public IActionResult GetOrders(int status) => Ok(new GetOrders(_context).Do(status));

		//[HttpGet("orders/{id}")]
		//public IActionResult GetOrder(Guid id) => Ok(new GetOrder(_context).Do(id));

		//[HttpPut("orders/{id}")]
		//public async Task<IActionResult> UpdateOrder(Guid id) => Ok((await new UpdateOrder(_context).Do(id)));
	}
}
