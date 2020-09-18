using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.StockAdmin;
using System;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
	[Route("[controller]")]
	[Authorize(Policy = "Manager")]
	public class StocksController : Controller
	{
		[HttpGet("")]
		public IActionResult GetStocks([FromServices] GetStock getStock) => 
			Ok(getStock.Do());

		[HttpPost("")]
		public async Task<IActionResult> CreateStocks(
				[FromBody] CreateStock.Request request,
				[FromServices] CreateStock createStock) => 
			Ok((await createStock.DoAsync(request)));

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteStocks(
				Guid id,
				[FromServices] DeleteStock deleteStock) => 
			Ok((await deleteStock.DoAsync(id)));

		[HttpPut("")]
		public async Task<IActionResult> UpdateStocks(
				[FromBody] UpdateStock.Request request,
				[FromServices] UpdateStock updateStock) => 
			Ok((await updateStock.DoAsync(request)));
	}
}
