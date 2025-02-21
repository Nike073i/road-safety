using System.Reflection;

namespace RoadSafety.Users.Contracts
{
	public interface IContractsProjectMarker
	{
		static Assembly Assembly => typeof(IContractsProjectMarker).Assembly;
	}
}
