using Silos.Users.Domain.Events;

namespace Silos.Users.Infrastructure.Projections;

public class UserDetailsProjection : SingleStreamAggregation<UserDetails>
{
    public UserDetailsProjection()
    {
        ProjectEvent<UserRegistered>((item, @event) => item.Apply(@event));
        ProjectEvent<UserUpdated>((item, @event) => item.Apply(@event));
    }
}

//https://martendb.io/events/projections/aggregate-projections.html#aggregate-by-stream