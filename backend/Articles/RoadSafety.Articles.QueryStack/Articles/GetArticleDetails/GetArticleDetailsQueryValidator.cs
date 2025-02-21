using FluentValidation;

namespace RoadSafety.Articles.QueryStack.Articles.GetArticleDetails
{

	public class GetArticleDetailsQueryValidator : AbstractValidator<GetArticleDetailsQuery>
	{
		public GetArticleDetailsQueryValidator()
		{
			RuleFor(q => q.Id).NotEmpty();
		}
	}
}
