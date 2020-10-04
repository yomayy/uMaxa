using Microsoft.AspNetCore.Mvc;
using Shop.Application.ShopUser;

namespace Shop.UI.Controllers
{
	[Route("[controller]")]
	public class UserOrderController : Controller
	{
		public IActionResult Index() {
			return View();
		}

		[HttpGet("")]
		public IActionResult GetOrders(
			string email,
			[FromServices] GetUserOrders getUserOrders) =>
				Ok(getUserOrders.Do(email));

	}
}
