namespace RoadSafety.Users.Domain.Roles
{
	public record Role
	{
		public int Code { get; private set; }
		public string Name { get; private set; }

#pragma warning disable CS8618
		private Role() { }
#pragma warning restore CS8618

		public static readonly Role User = new() { Code = 1, Name = nameof(User) };
		public static readonly Role Moderator = new() { Code = 2, Name = nameof(Moderator) };
		public static readonly IReadOnlyCollection<Role> All = [User, Moderator];

		public static Role FromName(string name) => All.First(role => role.Name == name);
	}
}
