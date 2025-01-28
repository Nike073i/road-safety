namespace RoadSafety.BuildingBlocks.Application.Validation
{
	public class ValidationException(IEnumerable<ValidationError> errors) : Exception
	{
		public IReadOnlyCollection<ValidationError> Errors { get; } = errors.ToList();
	}
}
