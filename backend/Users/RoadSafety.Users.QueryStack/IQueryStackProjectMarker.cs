using System.Reflection;

namespace RoadSafety.Users.QueryStack
{
	public interface IQueryStackProjectMarker
	{
		static Assembly Assembly => typeof(IQueryStackProjectMarker).Assembly;
	}
}
