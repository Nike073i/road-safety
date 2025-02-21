using System.Reflection;

namespace RoadSafety.Articles.Infrastructure
{
	public interface IInfrastructureProjectMarker
	{
		static Assembly Assembly => typeof(IInfrastructureProjectMarker).Assembly;
	}
}
