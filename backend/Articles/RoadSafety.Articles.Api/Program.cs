using RoadSafety.Articles.Api;
using RoadSafety.Articles.Api.Configuration;
using RoadSafety.Articles.Api.Endpoints;
using RoadSafety.Articles.Infrastructure;
using RoadSafety.BuildingBlocks.Api.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.UseSerilog();

var configurationAccessor = new ConfigurationAccessor(builder.Configuration);

builder
	.Services.AddInfrastructure(
		configurationAccessor.DatabaseSettings,
		configurationAccessor.LongOperationRunnerOptions
	)
	.AddApi(configurationAccessor.JwtBearerSettings);

var app = builder.Build();
app.UseAuthentication().UseAuthorization();
app.MapEndpoints();
app.Run();
