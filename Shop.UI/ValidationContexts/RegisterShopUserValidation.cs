using FluentValidation;
using Shop.UI.Pages.Account;

namespace Shop.UI.ValidationContexts
{
	public class RegisterShopUserValidation
		: AbstractValidator<UserRegisterViewModel>
	{
		public RegisterShopUserValidation() {
			RuleFor(x => x.UserName).NotNull().MinimumLength(3);
			RuleFor(x => x.Password).NotNull().MinimumLength(5);
			RuleFor(x => x.Email).NotNull().EmailAddress();
			RuleFor(x => x.ConfirmPassword).NotNull()
				.Equal(x => x.Password).WithMessage("Passwords do not match");
		}
	}
}
