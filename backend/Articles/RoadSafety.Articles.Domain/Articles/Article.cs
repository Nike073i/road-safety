using System.Diagnostics.CodeAnalysis;
using ErrorOr;
using RoadSafety.Articles.Domain.Articles.Blocks;
using RoadSafety.Articles.Domain.Articles.Events;
using RoadSafety.BuildingBlocks.Domain;

namespace RoadSafety.Articles.Domain.Articles
{
	public class Article : Entity
	{
		public Guid AuthorId { get; private set; }
		public DateTimeOffset CreatedAt { get; private set; }
		public Visibility Visibility { get; private set; }
		public bool IsArchived { get; private set; }
		public bool IsEnableCommenting { get; private set; }
		public int PublicVersion { get; private set; }
		public int Version { get; private set; }
		public ArticleDraft Draft { get; private set; }
		public ArticleInfo Info { get; private set; }

#pragma warning disable CS8618
		private Article() { }
#pragma warning restore CS8618

		public static ErrorOr<ArticleCreatedDomainEvent> CreateDefault(
			Guid authorId,
			DateTimeOffset currentUtc,
			string title,
			string? subtitle,
			string? image,
			string[] tags,
			ArticleBlock[] blocks,
			bool isEnableCommenting = true
		) =>
			new ArticleCreatedDomainEvent
			{
				ArticleId = Guid.NewGuid(),
				OccuredOn = currentUtc,
				AuthorId = authorId,
				CreatedAt = currentUtc,
				Visibility = Visibility.Hidden,
				IsEnableCommenting = isEnableCommenting,
				Title = title,
				Subtitle = subtitle,
				Image = image,
				Tags = tags,
				Blocks = blocks,
			};

		[SetsRequiredMembers]
		public Article(ArticleCreatedDomainEvent domainEvent)
		{
			Id = domainEvent.ArticleId;
			AuthorId = domainEvent.AuthorId;
			CreatedAt = domainEvent.CreatedAt;
			Visibility = domainEvent.Visibility;
			Draft = new ArticleDraft(domainEvent);
			Info = new ArticleInfo(domainEvent);
		}

		public static Article Create(ArticleCreatedDomainEvent domainEvent) => new(domainEvent);

		public ErrorOr<BlockInsertDomainEvent> InsertBlockBefore(
			Guid userId,
			DateTimeOffset currentUtc,
			ArticleBlock block,
			int position
		) =>
			userId
				.ToErrorOr()
				.FailIf(id => AuthorId != id, ArticleErrors.UserHaveNotRightsError)
				.FailIf(_ => IsArchived, ArticleErrors.ArticleIsArchivedError)
				.Then(_ => Draft.CanInsertBlockBefore(position))
				.Then(_ => new BlockInsertDomainEvent
				{
					OccuredOn = currentUtc,
					Block = block,
					Position = position,
				});

		public ErrorOr<BlockMovedAfterDomainEvent> MoveBlockAfter(
			Guid userId,
			DateTimeOffset currentUtc,
			int currentPosition,
			int afterPosition
		) =>
			userId
				.ToErrorOr()
				.FailIf(id => AuthorId != id, ArticleErrors.UserHaveNotRightsError)
				.FailIf(_ => IsArchived, ArticleErrors.ArticleIsArchivedError)
				.Then(_ => Draft.CanMoveBlockAfter(currentPosition, afterPosition))
				.Then(_ => new BlockMovedAfterDomainEvent
				{
					OccuredOn = currentUtc,
					CurrentPosition = currentPosition,
					AfterPosition = afterPosition,
				});

		public ErrorOr<BlockUpdatedDomainEvent> UpdateBlock(
			Guid userId,
			DateTimeOffset currentUtc,
			ArticleBlock block,
			int position
		) =>
			userId
				.ToErrorOr()
				.FailIf(id => AuthorId != id, ArticleErrors.UserHaveNotRightsError)
				.FailIf(_ => IsArchived, ArticleErrors.ArticleIsArchivedError)
				.Then(_ => Draft.CanUpdateBlock(position))
				.Then(_ => new BlockUpdatedDomainEvent
				{
					OccuredOn = currentUtc,
					Position = position,
					Block = block,
				});

		public ErrorOr<BlockRemovedDomainEvent> RemoveBlock(
			Guid userId,
			DateTimeOffset currentUtc,
			int position
		) =>
			userId
				.ToErrorOr()
				.FailIf(id => AuthorId != id, ArticleErrors.UserHaveNotRightsError)
				.FailIf(_ => IsArchived, ArticleErrors.ArticleIsArchivedError)
				.Then(_ => Draft.CanRemoveBlock(position))
				.Then(_ => new BlockRemovedDomainEvent
				{
					OccuredOn = currentUtc,
					Position = position,
				});

		public ErrorOr<ArticleInfoChangedDomainEvent> UpdateInfo(
			Guid userId,
			DateTimeOffset currentUtc,
			string title,
			string? subtitle,
			string? image,
			string[] tags
		) =>
			userId
				.ToErrorOr()
				.FailIf(id => AuthorId != id, ArticleErrors.UserHaveNotRightsError)
				.FailIf(_ => IsArchived, ArticleErrors.ArticleIsArchivedError)
				.Then(_ => new ArticleInfoChangedDomainEvent
				{
					OccuredOn = currentUtc,
					Title = title,
					Tags = tags,
					Image = image,
					Subtitle = subtitle,
				});

		public ErrorOr<ArticleVisibilityChangedDomainEvent> SetVisibility(
			Guid userId,
			Visibility newVisibility,
			DateTimeOffset currentUtc
		) =>
			userId
				.ToErrorOr()
				.FailIf(id => AuthorId != id, ArticleErrors.UserHaveNotRightsError)
				.Then(_ => new ArticleVisibilityChangedDomainEvent
				{
					OccuredOn = currentUtc,
					Visibility = newVisibility,
				});

		public ErrorOr<ArticleCommentingSettingsChangedDomainEvent> SetEnableCommenting(
			Guid userId,
			bool isEnable,
			DateTimeOffset currentUtc
		) =>
			userId
				.ToErrorOr()
				.FailIf(id => AuthorId != id, ArticleErrors.UserHaveNotRightsError)
				.Then(_ => new ArticleCommentingSettingsChangedDomainEvent
				{
					OccuredOn = currentUtc,
					IsEnableCommenting = isEnable,
				});

		public ErrorOr<ArticleArchivedDomainEvent> Archive(
			Guid userId,
			DateTimeOffset currentUtc
		) =>
			userId
				.ToErrorOr()
				.FailIf(id => AuthorId != id, ArticleErrors.UserHaveNotRightsError)
				.FailIf(_ => IsArchived, ArticleErrors.ArticleIsArchivedError)
				.Then(_ => new ArticleArchivedDomainEvent { OccuredOn = currentUtc });

		public ErrorOr<ArticleContentUpdated> UpdateContent(
			Guid userId,
			DateTimeOffset currentUtc
		) =>
			userId
				.ToErrorOr()
				.FailIf(id => AuthorId != id, ArticleErrors.UserHaveNotRightsError)
				.FailIf(_ => PublicVersion == Version, ArticleErrors.NoEventsToUpdateContentError)
				.Then(_ => new ArticleContentUpdated
				{
					// Включаем в публичную версию событие обновления версии, чтобы избежать возможности бесконечно обновлять контент
					PublicVersion = Version + 1,
					OccuredOn = currentUtc,
				});

		public void Apply(ArticleVisibilityChangedDomainEvent domainEvent) =>
			Visibility = domainEvent.Visibility;

		public void Apply(ArticleCommentingSettingsChangedDomainEvent domainEvent) =>
			IsEnableCommenting = domainEvent.IsEnableCommenting;

		public void Apply(ArticleArchivedDomainEvent _) => IsArchived = true;

		public void Apply(ArticleContentUpdated domainEvent) =>
			PublicVersion = domainEvent.PublicVersion;

		public void Apply(ArticleInfoChangedDomainEvent domainEvent) => Info.Apply(domainEvent);

		public void Apply(BlockInsertDomainEvent domainEvent) => Draft.Apply(domainEvent);

		public void Apply(BlockMovedAfterDomainEvent domainEvent) => Draft.Apply(domainEvent);

		public void Apply(BlockRemovedDomainEvent domainEvent) => Draft.Apply(domainEvent);

		public void Apply(BlockUpdatedDomainEvent domainEvent) => Draft.Apply(domainEvent);
	}
}
