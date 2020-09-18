using FluentValidation;
using Shop.UI.Pages.Account;

namespace Shop.UI.ValidationContexts
{
	public class LoginUserValidation
		:AbstractValidator<LoginViewModel>
	{
		public LoginUserValidation() {
			RuleFor(x => x.Username).NotNull().MinimumLength(3);
			RuleFor(x => x.Password).NotNull().MinimumLength(5);
		}
	}
}
