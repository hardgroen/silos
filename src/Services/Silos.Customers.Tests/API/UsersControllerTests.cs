using Silos.Users.API.Controllers;
using Silos.Users.Domain.Commands;
using Silos.Users.Domain.Events;
using Silos.Users.Infrastructure.Projections;

namespace Silos.Customers.Tests;

public class UsersControllerTests
{
    public UsersControllerTests()
    {
        var fakeToken = "yJhbGciOiJSUzI1NiIsImtpZCI6IjAzOUI0NUE1OThCMzE3RTRBQzc0M";
        _tokenRequester
            .Setup(m => m.GetUserTokenFromHttpContextAsync())
            .ReturnsAsync(fakeToken);

        _customersController = new UsersController(
            _tokenRequester.Object,
            _commandBus.Object,
            _queryBus.Object);
    }

    [Fact]
    public async Task GetDetails_WithCustomerId_ShouldReturnGetCustomerDetails()
    {
        // Given
        var customerId = Guid.NewGuid();
        var expectedData = new UserDetails
        {
            Id = customerId,
            Email = "customer@test.com",
            Name = "CustomerX"
        };

        _queryBus
            .Setup(m => m.Send(It.IsAny<GetUserDetails>()))
            .ReturnsAsync(expectedData);

        // When
        var response = await _customersController.GetDetails();

        // Then
        response.Should().BeOfType<OkObjectResult>()
            .Subject.Value.Should().BeOfType<ApiResponse<UserDetails>>()
            .Subject.Data.Should().BeEquivalentTo(expectedData);
    }

    [Fact]
    public async Task ListHistory_WithCustomerId_ShouldReturnListOfCustomerEventHistory()
    {
        // Given
        var customerId = Guid.NewGuid();
        var expectedData = new List<UserEventHistory>
        {
            new UserEventHistory(
                Guid.NewGuid(),
                customerId,
                typeof(UserRegistered).Name,
                "event data"
            ),
            new UserEventHistory(
                Guid.NewGuid(),
                customerId,
                typeof(UserUpdated).Name,
                "event data"
            )
        };

        _queryBus
            .Setup(m => m.Send(It.IsAny<GetUserEventHistory>()))
            .ReturnsAsync(expectedData);

        // When
        var response = await _customersController.ListHistory(customerId);

        // Then
        response.Should().BeOfType<OkObjectResult>()
            .Subject.Value.Should().BeOfType<ApiResponse<IList<UserEventHistory>>>()
            .Subject.Data.Should().BeEquivalentTo(expectedData);
    }

    [Fact]
    public async Task Register_RegisterCustomerRequest_ShouldRegisterCustomer()
    {
        // Given
        var request = new RegisterUserRequest()
        {
            Email = "customer@test.com",
            Name = "CustomerX",
            Password = "p4$$w0rd",
            PasswordConfirm = "p4$$w0rd"
        };

        _commandBus
            .Setup(m => m.Send(It.IsAny<RegisterUser>()));

        // When
        var response = await _customersController.Register(request);

        // Then
        response.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task UpdateInformation_RegisterCustomerRequest_ShouldUpdateInformation()
    {
        // Given
        var customerId = Guid.NewGuid();
        var request = new UpdateUserRequest
        {
            Name = "CustomerX"
        };

        _commandBus
            .Setup(m => m.Send(It.IsAny<RegisterUser>()));

        // When
        var response = await _customersController.UpdateInformation(customerId, request);

        // Then
        response.Should().BeOfType<OkObjectResult>();
    }

    private Mock<ICommandBus> _commandBus = new();
    private Mock<IQueryBus> _queryBus = new();
    private UsersController _customersController;
    private Mock<ITokenRequester> _tokenRequester = new();
}