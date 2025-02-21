using System.Text.Json.Serialization.Metadata;

namespace RoadSafety.BuildingBlocks.Infrastructure.Serialization
{
	public class PrivateConstructorSerializationOptionsConfigurer<T>
		: ITypeSerializationOptionsConfigurer
	{
		public Type Target => typeof(T);

		public void Configure(JsonTypeInfo jsonTypeInfo)
		{
			jsonTypeInfo.CreateObject = () =>
				Activator.CreateInstance(Target, true)
				?? throw new ApplicationException("Serialization not supported");
		}
	}
}
