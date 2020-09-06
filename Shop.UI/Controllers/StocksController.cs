using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.StockAdmin;
using Shop.Database;
using System;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
	[Route("[controller]")]
	[Authorize(Policy = "Manager")]
	public class StocksController : Controller
	{

		private ApplicationDbContext _context;

		public StocksController(ApplicationDbContext context) {
			_context = context;
		}

		[HttpGet("")]
		public IActionResult GetStocks() => Ok(new GetStock(_context).Do());

		[HttpPost("")]
		public async Task<IActionResult> CreateStocks([FromBody] CreateStock.Request request)
			=> Ok((await new CreateStock(_context).Do(request)));

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteStocks(Guid id) => Ok((await new DeleteStock(_context).Do(id)));

		[HttpPut("")]
		public async Task<IActionResult> UpdateStocks([FromBody] UpdateStock.Request request)
			=> Ok((await new UpdateStock(_context).Do(request)));
	}
}
