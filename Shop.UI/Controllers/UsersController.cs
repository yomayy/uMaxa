using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.ProductsAdmin;
using Shop.Application.StockAdmin;
using Shop.Application.UsersAdmin;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
	[Route("[controller]")]
	[Authorize(Policy = "Admin")]
	public class UsersController : Controller
	{
		private CreateUser _createUser;

		public UsersController(CreateUser createUser) {
			_createUser = createUser;
		}
		public async Task<IActionResult> CreateUser([FromBody]CreateUser.Request request) {
			await _createUser.Do(request);

			return Ok();
		}

		#region Product

		//[HttpGet("products")]
		//public IActionResult GetProducts() => Ok(new GetProducts(_context).Do());

		//[HttpGet("products/{id}")]
		//public IActionResult GetProduct(Guid id) => Ok(new GetProduct(_context).Do(id));

		//[HttpPost("products")]
		//public async Task<IActionResult> CreateProduct([FromBody] CreateProduct.Request request)
		//	=> Ok((await new CreateProduct(_context).Do(request)));

		//[HttpDelete("products/{id}")]
		//public async Task<IActionResult> DeleteProduct(Guid id) => Ok((await new DeleteProduct(_context).Do(id)));

		//[HttpPut("products")]
		//public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct.Request request)
		//	=> Ok((await new UpdateProduct(_context).Do(request)));

		#endregion

		#region Stock

		//[HttpGet("stocks")]
		//public IActionResult GetStocks() => Ok(new GetStock(_context).Do());

		//[HttpPost("stocks")]
		//public async Task<IActionResult> CreateStocks([FromBody] CreateStock.Request request)
		//	=> Ok((await new CreateStock(_context).Do(request)));

		//[HttpDelete("stocks/{id}")]
		//public async Task<IActionResult> DeleteStocks(Guid id) => Ok((await new DeleteStock(_context).Do(id)));

		//[HttpPut("stocks")]
		//public async Task<IActionResult> UpdateStocks([FromBody] UpdateStock.Request request)
		//	=> Ok((await new UpdateStock(_context).Do(request)));

		#endregion

	}
}
