using RoadSafety.BuildingBlocks.CommandStack.Cqrs;

namespace RoadSafety.Articles.CommandStack.Articles.Create
{
	public record CreateCommand(string Title, string? Subtitle, string? Image, string[] Tags)
		: ICommand<Guid>;
}
