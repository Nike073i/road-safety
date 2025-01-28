using RoadSafety.BuildingBlocks.Api.Logging;
using RoadSafety.Users.Api;
using RoadSafety.Users.Api.Configuration;
using RoadSafety.Users.Api.Endpoints;
using RoadSafety.Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.UseSerilog();

var configurationAccessor = new ConfigurationAccessor(builder.Configuration);

builder
	.Services.AddInfrastructure(
		configurationAccessor.RabbitMqSettings,
		configurationAccessor.DatabaseSettings,
		configurationAccessor.KeycloakSettings,
		configurationAccessor.AdminServiceAccountSetting
	)
	.AddApi(configurationAccessor.JwtBearerSettings);

var app = builder.Build();
app.UseAuthentication().UseAuthorization();
app.MapEndpoints().MapGrpc();
app.Run();
