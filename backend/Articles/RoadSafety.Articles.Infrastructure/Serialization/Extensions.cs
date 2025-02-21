using Microsoft.Extensions.DependencyInjection;
using RoadSafety.Articles.CommandStack.Articles.UpdateContent;
using RoadSafety.Articles.Domain.Articles;
using RoadSafety.BuildingBlocks.Infrastructure.LongOperations.Serialization;
using RoadSafety.BuildingBlocks.Infrastructure.Serialization;

namespace RoadSafety.Articles.Infrastructure.Serialization
{
	public static class Extensions
	{
		public static IServiceCollection AddSerializationServices(this IServiceCollection services)
		{
			var builder = new LongOperationSerializationOptionsConfigurerBuilder();
			builder.Add<UpdateContentOperation>();
			var longOperationsConfigurer = builder.Build();

			services.AddSingleton(longOperationsConfigurer);
			services.AddSingleton<
				ITypeSerializationOptionsConfigurer,
				VisibilitySerializationOptionsConfigurer
			>();
			services.AddSingleton<
				ITypeSerializationOptionsConfigurer,
				PrivateConstructorSerializationOptionsConfigurer<Visibility>
			>();

			services.AddSerialization();
			return services;
		}
	}
}
