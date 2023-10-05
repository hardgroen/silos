using Silos.Users.Infrastructure.Projections;

namespace Silos.Users.Application.GettingUserDetails;

public class GetUserDetailsHandler : IQueryHandler<GetUserDetails, UserDetails>
{
    private readonly TokenIssuerSettings _tokenIssuerSettings;
    private readonly IQuerySession _querySession;
    private readonly IHttpRequester _requester;

    public GetUserDetailsHandler(
        IOptions<TokenIssuerSettings> tokenIssuerSettings,
        IHttpRequester httpRequester,
        IQuerySession querySession)
    {
        _tokenIssuerSettings = tokenIssuerSettings.Value;
        _requester = httpRequester;
        _querySession = querySession;
    }

    public async Task<UserDetails> Handle(GetUserDetails query, CancellationToken cancellationToken)
    {
        var uri = $"{_tokenIssuerSettings.Authority}/connect/userinfo";

        var response = await _requester
            .GetAsync<UserInfoResponse>(uri, query.UserAccessToken);

        if (response is null)
            throw new RecordNotFoundException($"Cannot retrieve user info.");

        var details = new UserDetails();
        var customer = _querySession.Query<UserDetails>()
            .FirstOrDefault(c => c.Email == response.Email);

        if (customer is null)
            throw new RecordNotFoundException($"User not found.");

        details.Id = customer.Id;
        details.Email = customer.Email;
        details.Name = customer.Name;

        return details;
    }

    public record class UserInfoResponse(string Email);
}
