using System.Diagnostics.CodeAnalysis;
using ErrorOr;
using RoadSafety.Articles.Domain.Articles.Blocks;
using RoadSafety.Articles.Domain.Articles.Events;

namespace RoadSafety.Articles.Domain.Articles
{
	public class ArticleDraft
	{
		private readonly List<ArticleBlock> _blocks;

		public IReadOnlyCollection<ArticleBlock> Blocks => _blocks;

#pragma warning disable CS8618
		private ArticleDraft() { }
#pragma warning restore CS8618

		[SetsRequiredMembers]
		public ArticleDraft(ArticleCreatedDomainEvent domainEvent)
		{
			_blocks = domainEvent.Blocks.ToList();
		}

		public ErrorOr<Success> CanInsertBlockBefore(int position) =>
			position
				.ToErrorOr()
				.FailIf(
					pos => pos > _blocks.Count || pos < 0,
					ArticleErrors.BlockIncorrectPositionError
				)
				.Then(_ => Result.Success);

		public ErrorOr<Success> CanMoveBlockAfter(int currentPosition, int afterPosition) =>
			currentPosition
				.ToErrorOr()
				.FailIf(
					_ => !IsCorrectPosition(currentPosition),
					ArticleErrors.BlockIncorrectPositionError
				)
				// -1 - Insert in the head
				.FailIf(
					_ => !IsCorrectPosition(afterPosition, -1),
					ArticleErrors.BlockIncorrectPositionError
				)
				.FailIf(
					_ => Math.Abs(currentPosition - afterPosition) <= 1,
					ArticleErrors.BlockIncorrectPositionError
				)
				.Then(_ => Result.Success);

		public ErrorOr<Success> CanUpdateBlock(int position) =>
			position
				.ToErrorOr()
				.FailIf(
					_ => !IsCorrectPosition(position),
					ArticleErrors.BlockIncorrectPositionError
				)
				.Then(_ => Result.Success);

		public ErrorOr<Success> CanRemoveBlock(int position) =>
			position
				.ToErrorOr()
				.FailIf(
					_ => !IsCorrectPosition(position),
					ArticleErrors.BlockIncorrectPositionError
				)
				.Then(_ => Result.Success);

		private bool IsCorrectPosition(int position, int minimalPosition = 0) =>
			position < _blocks.Count && position >= minimalPosition;

		public void Apply(BlockInsertDomainEvent domainEvent) =>
			_blocks.Insert(domainEvent.Position, domainEvent.Block);

		public void Apply(BlockMovedAfterDomainEvent domainEvent)
		{
			int currentPosition = domainEvent.CurrentPosition;
			int afterPosition = domainEvent.AfterPosition;

			var block = _blocks[currentPosition];
			_blocks.Remove(block);

			if (currentPosition > afterPosition)
				afterPosition++;

			_blocks.Insert(afterPosition, block);
		}

		public void Apply(BlockUpdatedDomainEvent domainEvent) =>
			_blocks[domainEvent.Position] = domainEvent.Block;

		public void Apply(BlockRemovedDomainEvent domainEvent) =>
			_blocks.RemoveAt(domainEvent.Position);
	}
}
