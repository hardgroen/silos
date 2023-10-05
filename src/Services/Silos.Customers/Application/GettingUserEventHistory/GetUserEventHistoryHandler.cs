namespace Silos.Users.Application.GettingUserEventHistory;

public class GetUserEventHistoryHandler : IQueryHandler<GetUserEventHistory, IList<UserEventHistory>>
{
    private readonly IQuerySession _querySession;

    public GetUserEventHistoryHandler(
        IQuerySession querySession)
    {
        _querySession = querySession;
    }

    public Task<IList<UserEventHistory>> Handle(GetUserEventHistory query, CancellationToken cancellationToken)
    {
        var customerHistory = _querySession.Query<UserEventHistory>()
           .Where(c => c.AggregateId == query!.UserId.Value);

        if (customerHistory is null)
            throw new RecordNotFoundException($"History for User {query.UserId.Value} was not found.");

        return Task.FromResult<IList<UserEventHistory>>(customerHistory.ToList());
    }
}
