using Silos.Users.Domain;
using Silos.Users.Infrastructure.Projections;

namespace Silos.Users.Application.RegisteringUser;

public class EmailUniquenessChecker : IEmailUniquenessChecker
{
    private readonly IQuerySession _querySession;

    public EmailUniquenessChecker(IQuerySession querySession)
    {
        _querySession = querySession;
    }

    public bool IsUnique(string customerEmail)
    {
        var customer = _querySession.Query<UserDetails>()
            .FirstOrDefault(c => c.Email == customerEmail);

        return customer is null;
    }
}