using System.Text.Json.Serialization.Metadata;

namespace RoadSafety.BuildingBlocks.Infrastructure.Serialization
{
	public interface ITypeSerializationOptionsConfigurer
	{
		Type Target { get; }
		void Configure(JsonTypeInfo jsonTypeInfo);
	}
}
