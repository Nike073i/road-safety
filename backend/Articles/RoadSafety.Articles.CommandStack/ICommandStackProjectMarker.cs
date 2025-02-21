using System.Reflection;

namespace RoadSafety.Articles.CommandStack
{
	public interface ICommandStackProjectMarker
	{
		static Assembly Assembly => typeof(ICommandStackProjectMarker).Assembly;
	}
}
