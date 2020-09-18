using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Application.Products;
using System.Threading.Tasks;

namespace Shop.UI.Pages
{
	public class ProductModel : PageModel
    {
        [BindProperty]
		public AddToCart.Request CartViewModel { get; set; }

        public GetProduct.ProductViewModel Product { get; set; }

        public async Task<IActionResult> OnGet(
                string name,
                [FromServices] GetProduct getProduct)
        {
            Product = await getProduct.DoAsync(name.Replace("-", " "));
            if(Product == null) {
                return RedirectToPage("Index");
			}
			else {
                return Page();
			}
        }

        public async Task<IActionResult> OnPost(
            [FromServices] AddToCart addToCart) {
            var stockAdded = await addToCart.DoAsync(CartViewModel);
			if (stockAdded) {
                return RedirectToPage("Cart");
			}
			else {
                // TODO: add a warning
                return RedirectToPage("Error");
			}
		}
    }
}
