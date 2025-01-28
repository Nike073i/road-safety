using ErrorOr;
using Microsoft.Extensions.Logging;
using RoadSafety.BuildingBlocks.Domain;
using RoadSafety.Users.CommandStack.Persistence;
using RoadSafety.Users.Domain.Roles;
using RoadSafety.Users.Domain.Users;
using RoadSafety.Users.IntegrationEvents;

namespace RoadSafety.Users.CommandStack.Users.UserCreated
{
	public class CreateUserEventSubscriber(
		ILogger<CreateUserEventSubscriber> logger,
		IUserUnitOfWork unitOfWork
	) : IIntegrationEventSubscriber<UserCreatedIntegrationEvent>
	{
		private readonly Role[] _defaultRoles = [Role.User];
		private readonly ILogger<CreateUserEventSubscriber> _logger = logger;
		private readonly IUserUnitOfWork _unitOfWork = unitOfWork;

		public Task Handle(
			UserCreatedIntegrationEvent notification,
			CancellationToken cancellationToken
		) =>
			User.Create(notification.UserId, _defaultRoles)
				.ThenDoAsync(user => _unitOfWork.UserDao.Add(user))
				.ThenDoAsync(_ => _unitOfWork.CommitAsync())
				.Switch(
					onValue: _ => _logger.UserInitialized(),
					onError: errors =>
						_logger.InitializationError(
							string.Join("; ", errors.SelectMany(e => e.Description))
						)
				);
	}
}
