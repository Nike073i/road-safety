using FluentValidation;

namespace RoadSafety.Articles.Contracts.Common.Articles.GetArticleDetails
{
	public class GetArticleDetailsRequestValidator : AbstractValidator<GetArticleDetailsRequest>
	{
		public GetArticleDetailsRequestValidator()
		{
			RuleFor(x => x.Id).NotEmpty();
		}
	}
}
