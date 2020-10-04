using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application;
using Shop.Application.ShopUser;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.UI.Pages.Account
{
	public class UserLoginModel : PageModel
    {
        public void OnGet()
        {
        }

        [BindProperty]
        public UserLoginViewModel Input { get; set; }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostUserLogin(UserLoginModel model,
                [FromServices] GetShopUser getShopUser) {
            if (ModelState.IsValid) {
                //User user = await _context.Users
                //    .Include(u => u.Role)
                //    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                var user = getShopUser.Do(model.Input.Email, model.Input.Password);
                if (user != null) {

                    ShopUserViewModel userViewModel = new ShopUserViewModel {
                        Email = user?.Email,
                        RoleName = user?.Role?.Name
                    };

                    await Authenticate(userViewModel); // аутентификация

                    string url = Url.Page("VueLoginOrder", new { email = user.Email });
                    return Redirect(url);
                }
                ModelState.AddModelError("", "Invalid login or password");
                return Page();
            }
            return Page();
        }
        private async Task Authenticate(ShopUserViewModel user) {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }

    [Service]
    public class UserLoginViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ShopUserViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
