using FluentValidation;

namespace RoadSafety.Articles.Contracts.Moderator.Articles.UpdateContent
{
	public class UpdateContentRequestValidator : AbstractValidator<UpdateContentRequest>
	{
		public UpdateContentRequestValidator()
		{
			RuleFor(r => r.ArticleId).NotEmpty();
		}
	}
}
