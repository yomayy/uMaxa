using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.UI.ViewModels.Admin;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
	[Route("[controller]")]
	[Authorize(Policy = "Admin")]
	public class UsersController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;

		public UsersController(UserManager<IdentityUser> userManager) {
			_userManager = userManager;
		}

		[HttpPost("")]
		public async Task<IActionResult> CreateUser(
				[FromBody] CreateUserViewModel vm) {

			var managerUser = new IdentityUser() {
				UserName = vm.Username
			};

			await _userManager.CreateAsync(managerUser, "password");

			var managerClaim = new Claim("Role", "Manager");

			await _userManager.AddClaimAsync(managerUser, managerClaim);

			return Ok(new UserViewModel{
				Id = managerUser.Id,
				UserName = managerUser.UserName
			});
		}

		[HttpGet("")]
		public IActionResult GetUsers() {
			List<UserViewModel> userList = _userManager.Users
				.Select(x => new UserViewModel {
					Id = x.Id,
					UserName = x.UserName
				})
				.ToList();
			return Ok(userList);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(string id) {
			IdentityUser user = _userManager.Users.FirstOrDefault(x => x.Id == id);
			return Ok(await _userManager.DeleteAsync(user));
		}

		[HttpPut("")]
		public async Task<IActionResult> UpdateUser(
				[FromBody] UserViewModel user) {
			IdentityUser existingUser = _userManager.Users
				?.FirstOrDefault(x => x.Id == user.Id);
			existingUser.UserName = user.UserName;
			await _userManager.UpdateAsync(existingUser);
			return Ok(new UserViewModel {
				Id = existingUser.Id,
				UserName = existingUser.UserName
			});
		}
	}
}
