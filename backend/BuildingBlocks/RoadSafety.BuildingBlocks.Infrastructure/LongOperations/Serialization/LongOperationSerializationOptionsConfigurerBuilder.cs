using RoadSafety.BuildingBlocks.CommandStack.LongOperations;
using RoadSafety.BuildingBlocks.Infrastructure.Serialization;

namespace RoadSafety.BuildingBlocks.Infrastructure.LongOperations.Serialization
{
	public class LongOperationSerializationOptionsConfigurerBuilder
	{
		private readonly List<Type> _types = new();

		public LongOperationSerializationOptionsConfigurerBuilder Add<TLongOperation>()
			where TLongOperation : ILongOperation
		{
			_types.Add(typeof(TLongOperation));
			return this;
		}

		public ITypeSerializationOptionsConfigurer Build() =>
			new LongOperationSerializationOptionsConfigurer(_types);
	}
}
