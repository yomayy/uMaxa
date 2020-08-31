using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Shop.Application.Cart;
using Shop.Application.Orders;
using Shop.Database;
using Stripe;

namespace Shop.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
		public string PublicKey { get; }

		private ApplicationDbContext _context;

		public PaymentModel(IConfiguration configuration, ApplicationDbContext context) {
            PublicKey = configuration["Stripe:PublicKey"].ToString();
            _context = context;
        }

		public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Do();
            if (information == null) {
                return RedirectToPage("/Checkout/CustomerInformation");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(string stripeEmail, string stripeToken) {

            var customers = new CustomerService();
            var charges = new ChargeService();

            var CartOrder = new Application.Cart.GetOrder(HttpContext.Session, _context).Do();

            var customer = customers.Create(new CustomerCreateOptions {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions {
                Amount = CartOrder.GetTotalCharge(),
                Description = "Shop Purchase",
                Currency = "usd",
                Customer = customer.Id
            });

            await new CreateOrder(_context).Do(new CreateOrder.Request {
                StripeReference = charge.Id,
                FirstName = CartOrder.CustomerInformation.FirstName,
                LastName = CartOrder.CustomerInformation.LastName,
                Email = CartOrder.CustomerInformation.Email,
                PhoneNumber = CartOrder.CustomerInformation.PhoneNumber,
                Address1 = CartOrder.CustomerInformation.Address1,
                Address2 = CartOrder.CustomerInformation.Address2,
                City = CartOrder.CustomerInformation.City,
                PostCode = CartOrder.CustomerInformation.PostCode,
                Stocks = CartOrder.Products.Select(x => new CreateOrder.Stock {
                    StockId = x.StockId,
                    Quantity = x.Quantity
				}).ToList()
            });

            return RedirectToPage("/Index");
		}
    }
}
