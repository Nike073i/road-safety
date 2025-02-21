namespace RoadSafety.Articles.Domain.Articles.Blocks
{
	public class ArticleTextBlock : ArticleBlock
	{
		public string? Title { get; set; }
		public required string[] Paragraphs { get; set; }
	}
}
