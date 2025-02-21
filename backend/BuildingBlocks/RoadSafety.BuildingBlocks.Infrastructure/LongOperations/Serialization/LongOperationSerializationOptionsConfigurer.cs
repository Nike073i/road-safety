using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using RoadSafety.BuildingBlocks.CommandStack.LongOperations;
using RoadSafety.BuildingBlocks.Infrastructure.Serialization;

namespace RoadSafety.BuildingBlocks.Infrastructure.LongOperations.Serialization
{
	public class LongOperationSerializationOptionsConfigurer(List<Type> types)
		: ITypeSerializationOptionsConfigurer
	{
		public Type Target => typeof(ILongOperation);

		public void Configure(JsonTypeInfo jsonTypeInfo)
		{
			var options = new JsonPolymorphismOptions
			{
				TypeDiscriminatorPropertyName = "type",
				IgnoreUnrecognizedTypeDiscriminators = true,
				UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
			};
			types.ForEach(type => options.DerivedTypes.Add(new(type, type.Name)));
			jsonTypeInfo.PolymorphismOptions = options;
		}
	}
}
