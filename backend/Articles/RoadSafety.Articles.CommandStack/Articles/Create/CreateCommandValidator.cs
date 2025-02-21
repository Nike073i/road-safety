using FluentValidation;

namespace RoadSafety.Articles.CommandStack.Articles.Create
{
	public class CreateCommandValidator : AbstractValidator<CreateCommand>
	{
		public CreateCommandValidator()
		{
			RuleFor(c => c.Title).NotEmpty();
			RuleFor(c => c.Title).NotEmpty().When(c => !string.IsNullOrEmpty(c.Title));
			RuleFor(c => c.Image).Must(BeValidUri).When(c => !string.IsNullOrEmpty(c.Image));
			RuleFor(c => c.Tags).NotEmpty();
			RuleForEach(c => c.Tags).NotEmpty();
		}

		private static bool BeValidUri(string? uri) =>
			!string.IsNullOrEmpty(uri) && Uri.TryCreate(uri, UriKind.Absolute, out _);
	}
}
