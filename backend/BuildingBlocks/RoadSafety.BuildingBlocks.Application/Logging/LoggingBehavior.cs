using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using RoadSafety.BuildingBlocks.Application.Auth;
using Serilog.Context;

namespace RoadSafety.BuildingBlocks.Application.Logging
{
	public class LoggingBehavior<TRequest, TResponse>(
		ILogger<LoggingBehavior<TRequest, TResponse>> logger,
		IUserContext userContext
	) : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger = logger;
		private readonly IUserContext _userContext = userContext;

		public async Task<TResponse> Handle(
			TRequest request,
			RequestHandlerDelegate<TResponse> next,
			CancellationToken cancellationToken
		)
		{
			string requestName = request.GetType().Name;
			using var userIdContext = LogContext.PushProperty(
				"UserId",
				_userContext.UserInfo?.UserId
			);
			try
			{
				_logger.RequestReceived(requestName);

				var result = await next();

				if (result is IErrorOr errorOr && errorOr.IsError)
				{
					using var context = LogContext.PushProperty("Errors", errorOr.Errors, true);
					_logger.RequestCompletedWithBusinessError(requestName);
				}
				else
				{
					_logger.RequestCompletedSuccessfully(requestName);
				}
				return result;
			}
			catch (Exception exception)
			{
				_logger.RequestFailed(requestName, exception);
				throw;
			}
		}
	}
}
