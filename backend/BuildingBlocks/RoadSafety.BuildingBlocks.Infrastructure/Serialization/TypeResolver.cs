using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace RoadSafety.BuildingBlocks.Infrastructure.Serialization
{
	public class TypeResolver(IEnumerable<ITypeSerializationOptionsConfigurer> configurers)
		: DefaultJsonTypeInfoResolver
	{
		private readonly Dictionary<Type, ITypeSerializationOptionsConfigurer> _configurers =
			configurers.ToDictionary(c => c.Target);

		public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
		{
			var jsonTypeInfo = base.GetTypeInfo(type, options);
			var configurer = _configurers.GetValueOrDefault(jsonTypeInfo.Type);
			configurer?.Configure(jsonTypeInfo);

			return jsonTypeInfo;
		}
	}
}
