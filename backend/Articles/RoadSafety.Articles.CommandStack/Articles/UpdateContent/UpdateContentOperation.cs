using RoadSafety.BuildingBlocks.CommandStack.LongOperations;

namespace RoadSafety.Articles.CommandStack.Articles.UpdateContent
{
	public record UpdateContentOperation(Guid ArticleId, int Version) : ILongOperation;
}
