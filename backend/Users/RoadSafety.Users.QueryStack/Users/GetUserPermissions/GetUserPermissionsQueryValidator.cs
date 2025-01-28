using FluentValidation;

namespace RoadSafety.Users.QueryStack.Users.GetUserPermissions
{
	public class GetUserPermissionsQueryValidator : AbstractValidator<GetUserPermissionsQuery>
	{
		public GetUserPermissionsQueryValidator()
		{
			RuleFor(q => q.UserId).NotEmpty();
		}
	}
}
