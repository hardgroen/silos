namespace Silos.Users.Application.GettingUserEventHistory;

public record UserEventHistory(
    Guid Id,
    Guid AggregateId,
    string EventTypeName,
    string EventData) : IEventHistory
{
    public static UserEventHistory Create(IEvent @event, Guid aggregateId)
    {
        var serialized = JsonConvert.SerializeObject(@event.Data);
        return new UserEventHistory(
            Guid.NewGuid(),
            aggregateId,
            @event.EventTypeName,
            serialized);
    }
}