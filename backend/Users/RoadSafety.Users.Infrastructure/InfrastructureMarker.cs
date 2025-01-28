using System.Reflection;

namespace RoadSafety.Users.Infrastructure
{
	public interface IInfrastructureProjectMarker
	{
		static Assembly Assembly => typeof(IInfrastructureProjectMarker).Assembly;
	}
}
