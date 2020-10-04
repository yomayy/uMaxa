using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Security.Claims;

namespace Shop.UI.Pages.Account
{
	public class VueLoginOrderModel : PageModel
    {
		public string Email { get; set; }
		public void OnGet(string email = null)
        {
            Email = email;
        }

        public string GetSpecificClaim(ClaimsIdentity claimsIdentity, string claimType) {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);
            var rez = HttpContext.User.Claims;
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
