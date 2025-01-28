using System.Reflection;

namespace RoadSafety.Users.CommandStack
{
	public interface ICommandStackProjectMarker
	{
		static Assembly Assembly => typeof(ICommandStackProjectMarker).Assembly;
	}
}
