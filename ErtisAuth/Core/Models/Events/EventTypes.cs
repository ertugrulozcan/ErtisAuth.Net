namespace ErtisAuth.Core.Models.Events
{
	public enum EventTypes
	{
		Unknown,
		ApplicationCreatedEvent,
		ApplicationDeletedEvent,
		ApplicationUpdatedEvent,
		PasswordChangedEvent,
		PasswordResetEvent,
		RoleCreatedEvent,
		RoleDeletedEvent,
		RoleUpdatedEvent,
		TokenCreatedEvent,
		TokenRefreshedEvent,
		TokenRevokedEvent,
		UserCreatedEvent,
		UserDeletedEvent,
		UserUpdatedEvent,
		UserTypeUpdatedEvent
	}
}