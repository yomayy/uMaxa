using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application;
using System.Threading.Tasks;

namespace Shop.UI.Pages.Account
{
	public class LoginModel : PageModel
    {
		private SignInManager<IdentityUser> _signInManager;

		public LoginModel(SignInManager<IdentityUser> signInManager) {
            _signInManager = signInManager;
		}

        [BindProperty]
        public LoginViewModel Input { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost() {

			if (!ModelState.IsValid) {
				return Page();
			}

			var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, false, false);

			if (result.Succeeded) {
				return RedirectToPage("/Admin/Index");
			}
			else {
				return Page();
			}
		}

    }

	[Service]
    public class LoginViewModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
