using System.Reflection;

namespace RoadSafety.Articles.QueryStack
{
	public interface IQueryStackProjectMarker
	{
		static Assembly Assembly => typeof(IQueryStackProjectMarker).Assembly;
	}
}
