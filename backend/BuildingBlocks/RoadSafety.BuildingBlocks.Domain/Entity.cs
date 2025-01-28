namespace RoadSafety.BuildingBlocks.Domain
{
	public abstract class Entity<TId>
	{
		public required TId Id { get; init; }
	}

	public abstract class Entity : Entity<Guid>;
}
