using System.Reflection;

namespace RoadSafety.Articles.Contracts
{
	public interface IContractsProjectMarker
	{
		static Assembly Assembly => typeof(IContractsProjectMarker).Assembly;
	}
}
