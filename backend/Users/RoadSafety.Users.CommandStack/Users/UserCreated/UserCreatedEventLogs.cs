using Microsoft.Extensions.Logging;

namespace RoadSafety.Users.CommandStack.Users.UserCreated
{
	internal static partial class UserCreatedEventLogs
	{
		[LoggerMessage(
			Level = LogLevel.Information,
			Message = "The user has been initialized in the system",
			EventId = 1001001,
			EventName = "UserCreatedSubcriber.Initialized"
		)]
		public static partial void UserInitialized(this ILogger logger);

		[LoggerMessage(
			Level = LogLevel.Warning,
			Message = "The user has not been initialized in the system. Description - {Description}",
			EventId = 1001002,
			EventName = "UserCreatedSubcriber.InitializationError"
		)]
		public static partial void InitializationError(this ILogger logger, string description);
	}
}
