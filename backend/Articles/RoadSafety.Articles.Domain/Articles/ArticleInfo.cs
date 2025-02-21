using System.Diagnostics.CodeAnalysis;
using RoadSafety.Articles.Domain.Articles.Events;

namespace RoadSafety.Articles.Domain.Articles
{
	public class ArticleInfo
	{
		private string[] _tags;
		public string Title { get; private set; }
		public string? Subtitle { get; private set; }
		public string? Image { get; private set; }
		public IReadOnlyCollection<string> Tags => _tags;

#pragma warning disable CS8618
		private ArticleInfo() { }
#pragma warning restore CS8618

		[SetsRequiredMembers]
		public ArticleInfo(ArticleCreatedDomainEvent domainEvent)
		{
			Title = domainEvent.Title;
			Subtitle = domainEvent.Subtitle;
			Image = domainEvent.Image;
			_tags = domainEvent.Tags;
		}

		public void Apply(ArticleInfoChangedDomainEvent domainEvent)
		{
			Title = domainEvent.Title;
			Subtitle = domainEvent.Subtitle;
			Image = domainEvent.Image;
			_tags = domainEvent.Tags;
		}
	}
}
