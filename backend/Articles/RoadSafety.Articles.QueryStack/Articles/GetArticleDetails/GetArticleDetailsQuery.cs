using RoadSafety.BuildingBlocks.QueryStack.Cache;

namespace RoadSafety.Articles.QueryStack.Articles.GetArticleDetails
{
	public record GetArticleDetailsQuery(Guid Id) : CacheableQuery<GetArticleDetailsViewModel?>;
}
