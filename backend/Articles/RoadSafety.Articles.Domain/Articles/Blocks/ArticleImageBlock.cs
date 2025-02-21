namespace RoadSafety.Articles.Domain.Articles.Blocks
{
	public class ArticleImageBlock : ArticleBlock
	{
		public string? Title { get; set; }
		public required string Src { get; set; }
	}
}
