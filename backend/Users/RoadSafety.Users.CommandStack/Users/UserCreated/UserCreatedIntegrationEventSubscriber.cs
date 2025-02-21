using ErrorOr;
using Microsoft.Extensions.Logging;
using RoadSafety.BuildingBlocks.CommandStack.Persistence;
using RoadSafety.BuildingBlocks.Domain;
using RoadSafety.Users.CommandStack.Persistence;
using RoadSafety.Users.Domain.Roles;
using RoadSafety.Users.Domain.Users;
using RoadSafety.Users.IntegrationEvents;

namespace RoadSafety.Users.CommandStack.Users.UserCreated
{
	public class CreateUserEventSubscriber(
		ILogger<CreateUserEventSubscriber> logger,
		IUnitOfWork unitOfWork,
		IUserDao userDao
	) : IIntegrationEventSubscriber<UserCreatedIntegrationEvent>
	{
		private readonly Role[] _defaultRoles = [Role.User];

		public Task Handle(
			UserCreatedIntegrationEvent notification,
			CancellationToken cancellationToken
		) =>
			User.Create(notification.UserId, _defaultRoles)
				.ThenDoAsync(user => userDao.Add(user))
				.ThenDoAsync(_ => unitOfWork.CommitAsync())
				.Switch(
					onValue: _ => logger.UserInitialized(),
					onError: errors =>
						logger.InitializationError(
							string.Join("; ", errors.SelectMany(e => e.Description))
						)
				);
	}
}
