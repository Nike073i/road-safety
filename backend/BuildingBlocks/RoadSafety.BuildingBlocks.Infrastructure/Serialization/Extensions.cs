using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace RoadSafety.BuildingBlocks.Infrastructure.Serialization
{
	public static class Extensions
	{
		public static IServiceCollection AddSerialization(this IServiceCollection services) =>
			services
				.AddSingleton(sp =>
				{
					var configures = sp.GetServices<ITypeSerializationOptionsConfigurer>();
					return new JsonSerializerOptions
					{
						AllowOutOfOrderMetadataProperties = true,
						TypeInfoResolver = new TypeResolver(configures),
					};
				})
				.AddSingleton<ISerializer, Serializer>();
	}
}
