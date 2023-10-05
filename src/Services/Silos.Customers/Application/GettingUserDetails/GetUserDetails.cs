using Silos.Users.Infrastructure.Projections;

namespace Silos.Users.Application.GettingUserDetails;

public record class GetUserDetails : IQuery<UserDetails>
{
    public string UserAccessToken { get; private set; }

    public static GetUserDetails Create(string userAccessToken)
    {
        if (string.IsNullOrEmpty(userAccessToken))
            throw new ArgumentNullException(nameof(userAccessToken));

        return new GetUserDetails(userAccessToken);
    }

    private GetUserDetails(string userAccessToken)
    {
        UserAccessToken = userAccessToken;
    }
}