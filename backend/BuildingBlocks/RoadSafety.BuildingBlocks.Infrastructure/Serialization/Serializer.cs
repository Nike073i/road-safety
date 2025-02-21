using System.Text.Json;

namespace RoadSafety.BuildingBlocks.Infrastructure.Serialization
{
	public class Serializer(JsonSerializerOptions options) : ISerializer
	{
		public T? Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, options);

		public string Serialize<T>(T elem) => JsonSerializer.Serialize(elem, options);
	}
}
