using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using RoadSafety.Articles.Domain.Articles.Blocks;
using RoadSafety.BuildingBlocks.Infrastructure.Serialization;

namespace RoadSafety.Articles.Infrastructure.Serialization
{
	public class VisibilitySerializationOptionsConfigurer : ITypeSerializationOptionsConfigurer
	{
		public Type Target => typeof(ArticleBlock);

		public void Configure(JsonTypeInfo jsonTypeInfo)
		{
			jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
			{
				TypeDiscriminatorPropertyName = "type",
				IgnoreUnrecognizedTypeDiscriminators = true,
				UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
				DerivedTypes =
				{
					new JsonDerivedType(typeof(ArticleTextBlock), "TEXT"),
					new JsonDerivedType(typeof(ArticleImageBlock), "IMAGE"),
				},
			};
		}
	}
}
