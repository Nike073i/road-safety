using ErrorOr;
using Microsoft.AspNetCore.Http;

namespace RoadSafety.BuildingBlocks.Api.Endpoints
{
	public static class ErrorOrExtensions
	{
		public static Task<IResult> MatchResult<TValue>(
			this Task<ErrorOr<TValue>> errorOr,
			Func<TValue, IResult> onValue
		) => errorOr.MatchFirst(onValue, MapError);

		public static IResult MapError(Error error)
		{
			int statusCode = error.Type switch
			{
				ErrorType.Validation => StatusCodes.Status400BadRequest,
				ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
				ErrorType.Forbidden => StatusCodes.Status403Forbidden,
				ErrorType.NotFound => StatusCodes.Status404NotFound,
				ErrorType.Conflict => StatusCodes.Status409Conflict,
				ErrorType.Failure => StatusCodes.Status422UnprocessableEntity,
				ErrorType.Unexpected => StatusCodes.Status500InternalServerError,
				_ => StatusCodes.Status500InternalServerError,
			};
			return Results.Problem(statusCode: statusCode, detail: error.Description);
		}
	}
}
