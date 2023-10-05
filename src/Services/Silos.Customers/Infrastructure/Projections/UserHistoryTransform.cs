namespace Silos.Users.Infrastructure.Projections;

public class UserHistoryTransform : EventProjection
{
    public UserEventHistory Transform(IEvent<UserRegistered> @event) =>
        UserEventHistory.Create(@event, @event.Data.CustomerId);

    public UserEventHistory Transform(IEvent<UserUpdated> @event) =>
        UserEventHistory.Create(@event, @event.Data.UserId);
}
