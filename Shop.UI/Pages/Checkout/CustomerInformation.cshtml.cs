using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;

namespace Shop.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
		private IHostingEnvironment _environment;

		public CustomerInformationModel(IHostingEnvironment environment) {
            _environment = environment;
		}

        [BindProperty]
		public AddCustomerInformation.Request CustomerInformation { get; set; }

		public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Do();
            if(information == null) {
                #region DevEnvOnly
                if (_environment.IsDevelopment()) {
                    CustomerInformation = new AddCustomerInformation.Request {
                        FirstName = "devTest",
                        LastName = "devTest",
                        Email = "devTest@example.com",
                        PhoneNumber = "1111",
                        Address1 = "devTest",
                        Address2 = "devTest",
                        City = "devTest",
                        PostCode = "devTest"
                    };
                }
                #endregion
                return Page();
			}
			else {
                return RedirectToPage("/Checkout/Payment");
			}
        }

        public IActionResult OnPost() {
			if (!ModelState.IsValid) {
                return Page();
			}
            new AddCustomerInformation(HttpContext.Session).Do(CustomerInformation);

            return RedirectToPage("/Checkout/Payment");
        }
    }
}
