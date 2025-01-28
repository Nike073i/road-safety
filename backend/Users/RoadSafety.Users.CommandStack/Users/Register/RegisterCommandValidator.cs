using FluentValidation;
using RoadSafety.Users.Domain.Users;

namespace RoadSafety.Users.CommandStack.Users.Register
{
	public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
	{
		public RegisterCommandValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty();
			RuleFor(x => x.LastName).NotEmpty();
			RuleFor(x => x.UserName)
				.NotEmpty()
				.MinimumLength(UserConstraints.UserNameMinimalLength);
			RuleFor(x => x.Email).NotEmpty().EmailAddress();
			RuleFor(x => x.Password)
				.NotEmpty()
				.MinimumLength(UserConstraints.PasswordMinimalLength);
		}
	}
}
