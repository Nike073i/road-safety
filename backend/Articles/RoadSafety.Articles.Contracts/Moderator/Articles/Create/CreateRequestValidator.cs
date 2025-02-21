using FluentValidation;

namespace RoadSafety.Articles.Contracts.Moderator.Articles.Create
{
	public class CreateRequestValidator : AbstractValidator<CreateRequest>
	{
		public CreateRequestValidator()
		{
			RuleFor(r => r.Title).NotEmpty();
			RuleFor(r => r.Title).NotEmpty().When(r => !string.IsNullOrEmpty(r.Title));
			RuleFor(r => r.Image).Must(BeValidUri).When(r => !string.IsNullOrEmpty(r.Image));
			RuleFor(r => r.Tags).NotEmpty();
			RuleForEach(r => r.Tags).NotEmpty();
		}

		private static bool BeValidUri(string? uri) =>
			!string.IsNullOrEmpty(uri) && Uri.TryCreate(uri, UriKind.Absolute, out _);
	}
}
