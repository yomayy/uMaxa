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
	public class UserRegisterModel : PageModel
    {
        public void OnGet()
        {
        }

        [BindProperty]
        public UserRegisterViewModel Input { get; set; }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostUserRegister(UserRegisterModel model,
                [FromServices] GetShopUser getShopUser,
                [FromServices] CreateShopUser createShopUser) {
            if (ModelState.IsValid) {
                //var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                var user = getShopUser.Do(model.Input.Email);
                if (user == null) {
                    // добавляем пользователя в бд
                    var userResponse = createShopUser.Do(
                        model.Input.UserName, model.Input.Password, model.Input.Email);

                    ShopUserViewModel userViewModel = new ShopUserViewModel {
                        Email = userResponse?.Email,
                        RoleName = userResponse?.RoleName
                    };

                    await Authenticate(userViewModel); // аутентификация

                    return RedirectToPage("../Index");
                }
				else {
                    ModelState.AddModelError("", "already existing");
                    return Page();
                }
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
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", 
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }

    [Service]
    public class UserRegisterViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
