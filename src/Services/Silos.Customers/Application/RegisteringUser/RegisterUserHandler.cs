namespace Silos.Users.Application.RegisteringUser;

public class RegisterUserHandler : ICommandHandler<RegisterUser>
{
    private readonly IHttpRequester _httpRequester;
    private readonly IEmailUniquenessChecker _uniquenessChecker;
    private readonly TokenIssuerSettings _tokenIssuerSettings;
    private readonly IEventStoreRepository<User> _customerWriteRepository;

    public RegisterUserHandler(
        IHttpRequester httpRequester,
        IEmailUniquenessChecker uniquenessChecker,
        IOptions<TokenIssuerSettings> tokenIssuerSettings,
        IEventStoreRepository<User> customerWriteRepository)
    {
        _httpRequester = httpRequester;
        _uniquenessChecker = uniquenessChecker;
        _tokenIssuerSettings = tokenIssuerSettings.Value;
        _customerWriteRepository = customerWriteRepository;
    }

    public async Task Handle(RegisterUser command, CancellationToken cancellationToken)
    {
        if (!_uniquenessChecker.IsUnique(command.Email))
            throw new BusinessRuleException("This e-mail is already in use.");

        var customerData = new UserData(
            command.Email,
            command.Name);

        var customer = User.Create(customerData);
        var response = await CreateUserForCustomer(command);

        if (response is null)
            throw new RecordNotFoundException($"An error occurred creating the customer's user.");

        if (!response.Success)
            throw new RecordNotFoundException(response.Message);

        await _customerWriteRepository
            .AppendEventsAsync(customer);
    }

    private async Task<IntegrationHttpResponse> CreateUserForCustomer(RegisterUser command)
    {
        try
        {
            var identityServerCreateUserUrl = $"{_tokenIssuerSettings.Authority}/api/accounts/register";
            var result = await _httpRequester
                .PostAsync<IntegrationHttpResponse>(identityServerCreateUserUrl,
                new RegisterUserRequest(
                    command.Email,
                    command.Password,
                    command.PasswordConfirm));

            return result;
        }
        catch (Exception e)
        {
            throw new RecordNotFoundException(e.Message);
        }
    }

    private record RegisterUserRequest(string Email, string Password, string PasswordConfirm);
}