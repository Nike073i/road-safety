using ErrorOr;

namespace RoadSafety.BuildingBlocks.Domain.Extensions
{
	public static class ErrorOrExtensions
	{
		public static async Task<ErrorOr<TValue>> FailIf<TValue>(
			this Task<ErrorOr<TValue>> errorOr,
			Func<TValue, bool> onValue,
			Error error
		)
		{
			var result = await errorOr.ConfigureAwait(false);

			return result.FailIf(onValue, error);
		}
	}
}
