namespace RoadSafety.BuildingBlocks.Application.Validation
{
	public record ValidationError(string PropertyName, string ErrorMessage);
}
