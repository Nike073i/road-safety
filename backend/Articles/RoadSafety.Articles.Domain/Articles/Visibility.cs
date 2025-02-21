namespace RoadSafety.Articles.Domain.Articles
{
	public record Visibility
	{
		public string Name { get; private set; }

#pragma warning disable CS8618
		private Visibility() { }
#pragma warning restore CS8618

		public static readonly Visibility Visible = new() { Name = nameof(Visible) };
		public static readonly Visibility Hidden = new() { Name = nameof(Hidden) };
		public static readonly IReadOnlyCollection<Visibility> All = [Visible, Hidden];

		public static Visibility FromName(string name) => All.First(role => role.Name == name);
	}
}
