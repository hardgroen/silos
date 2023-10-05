using Silos.Users.Domain;

namespace Silos.Users.Application.GettingUserEventHistory;

public record class GetUserEventHistory : IQuery<IList<UserEventHistory>>
{
    public UserId UserId { get; private set; }

    public static GetUserEventHistory Create(UserId userId)
    {
        if (userId is null)
            throw new ArgumentNullException(nameof(userId));

        return new GetUserEventHistory(userId);
    }

    private GetUserEventHistory(UserId userId)
    {
        UserId = userId;
    }
}