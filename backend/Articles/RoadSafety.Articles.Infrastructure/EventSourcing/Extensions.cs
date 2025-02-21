using System.Text.Json;
using Marten;
using Marten.Events.Projections;
using Microsoft.Extensions.DependencyInjection;
using RoadSafety.Articles.CommandStack.EventSourcing;
using RoadSafety.Articles.Infrastructure.Articles.Projections;
using RoadSafety.BuildingBlocks.Infrastructure.Persistence;

namespace RoadSafety.Articles.Infrastructure.EventSourcing
{
	public static class Extensions
	{
		public static IServiceCollection AddEventSourcing(
			this IServiceCollection services,
			DatabaseSettings settings
		)
		{
			services.AddMarten(
				(sp) =>
				{
					var options = new StoreOptions();
					options.Connection(settings.СonnectionString);
					options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.None;
					options.DatabaseSchemaName = "articles";
					options.Projections.Add<ArticleCreatedProjection>(ProjectionLifecycle.Inline);
					options.Projections.Add<ArticleArchivedProjection>(ProjectionLifecycle.Inline);
					options.Projections.Add<ArticleVisibilityChangedProjection>(
						ProjectionLifecycle.Inline
					);
					options.Projections.Add<ArticleCommentingSettingsChangedProjection>(
						ProjectionLifecycle.Inline
					);
					options.UseSystemTextJsonForSerialization(
						sp.GetRequiredService<JsonSerializerOptions>()
					);
					return options;
				}
			);
			services.AddScoped<IEventStore, EventStore>();
			services.AddScoped<IEventContext, EventContext>();
			return services;
		}
	}
}
