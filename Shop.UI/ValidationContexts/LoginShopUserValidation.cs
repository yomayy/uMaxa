using FluentValidation;
using Shop.UI.Pages.Account;

namespace Shop.UI.ValidationContexts
{
	public class LoginShopUserValidation
		: AbstractValidator<UserLoginViewModel>
	{
		public LoginShopUserValidation() {
			RuleFor(x => x.Password).NotNull().MinimumLength(5);
			RuleFor(x => x.Email).NotNull().EmailAddress();
		}
	}
}
