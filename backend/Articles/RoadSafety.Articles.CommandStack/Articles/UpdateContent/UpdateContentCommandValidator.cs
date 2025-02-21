using FluentValidation;
using RoadSafety.BuildingBlocks.Application.Clock;

namespace RoadSafety.Articles.CommandStack.Articles.UpdateContent
{
	public class UpdateContentCommandValidator : AbstractValidator<UpdateContentCommand>
	{
		public UpdateContentCommandValidator(IDateTimeProvider dateTimeProvider)
		{
			RuleFor(c => c.ArticleId).NotEmpty();
			RuleFor(c => c.ScheduledTime)
				.GreaterThan(dateTimeProvider.UtcNow)
				.When(c => c.ScheduledTime.HasValue);
			RuleFor(c => c.Version).GreaterThan(0).When(c => c.Version.HasValue);
		}
	}
}
