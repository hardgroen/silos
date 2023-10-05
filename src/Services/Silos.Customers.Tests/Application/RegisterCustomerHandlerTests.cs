using Silos.Users.Application.RegisteringUser;
using Silos.Users.Domain;

namespace Silos.Customers.Tests.Application;

public class RegisterCustomerHandlerTests
{
    [Fact]
    public async Task Register_WithCommand_ShouldRegisterCustomer()
    {
        // Given
        _checker.Setup(p => p.IsUnique(It.IsAny<string>()))
            .Returns(true);
        
        var customerWriteRepository = new DummyEventStoreRepository<User>();

        var options = new Mock<IOptions<TokenIssuerSettings>>();
        options.Setup(p => p.Value)
            .Returns(new TokenIssuerSettings() { Authority = "http://url" });

        var requester = new Mock<IHttpRequester>();
        requester.Setup(p => p.PostAsync<IntegrationHttpResponse>(It.IsAny<string>(), It.IsAny<object>(), null))
            .Returns(Task.FromResult(new IntegrationHttpResponse() { Success = true }));

        var confirmation = _password;
        var registerCommand = RegisterUser.Create(_email, _password, confirmation, _name);
        var commandHandler = new RegisterUserHandler(
            requester.Object,
            _checker.Object, 
            options.Object,
            customerWriteRepository);

        // When
        await commandHandler.Handle(registerCommand, CancellationToken.None);

        // Then
        var addedCustomer = customerWriteRepository.AggregateStream.First().Aggregate;
        addedCustomer.Email.Should().Be(registerCommand.Email);
        addedCustomer.Name.Should().Be(registerCommand.Name);
    }

    [Fact]
    public async Task Register_WithExistingEmail_ShouldThrownException()
    {
        // Given       
        _checker.Setup(p => p.IsUnique(It.IsAny<string>()))
            .Returns(false);

        var customerWriteRepository = new DummyEventStoreRepository<User>();

        var options = new Mock<IOptions<TokenIssuerSettings>>();
        options.Setup(p => p.Value)
            .Returns(new TokenIssuerSettings() { Authority = "http://url" });

        var requester = new Mock<IHttpRequester>();
        requester.Setup(p => p.PostAsync<IntegrationHttpResponse>(It.IsAny<string>(), It.IsAny<object>(), null))
            .Returns(Task.FromResult(new IntegrationHttpResponse() { Success = true }));

        var confirmation = _password;
        var registerCommand = RegisterUser.Create(_email, _password, confirmation, _name);
        var commandHandler = new RegisterUserHandler(
            requester.Object,
            _checker.Object,
            options.Object,
            customerWriteRepository);

        // When
        Func<Task> action = async () =>
            await commandHandler.Handle(registerCommand, CancellationToken.None);

        // Then
        await action.Should().ThrowAsync<BusinessRuleException>();
    }

    public const string _email = "email@test.com";
    public const string _name = "UserTest";
    public const string _password = "p4ssw0rd";
    public const string _address = "Rue XYZ";
    public const decimal _creditLimit = 1000;
    private Mock<IEmailUniquenessChecker> _checker = new();
}