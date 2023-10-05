using Silos.Users.Domain;

namespace Silos.Customers.Tests.Domain;

public class CustomerEventsTests
{
    [Fact]
    public void CreatingCustomer_WithCustomerData_ReturnsCustomerRegisteredEvent()
    {
        // Given
        var customerData = new UserData(_email, _name);

        // When
        var customer = User.Create(customerData);

        // Then
        var @event = customer.GetUncommittedEvents().LastOrDefault() as UserRegistered;
        Assert.NotNull(@event);
        @event.Should().BeOfType<UserRegistered>();
    }

    [Fact]
    public void UpdatingCustomer_WithCustomerData_ReturnsCustomerUpdatedEvent()
    {
        // Given
        var customerData = new UserData(_email, _name);
        var customer = User.Create(customerData);

        // When
        customer.UpdateCustomerInformation(customerData);

        // Then
        var @event = customer.GetUncommittedEvents().LastOrDefault() as UserUpdated;
        Assert.NotNull(@event);
        @event.Should().BeOfType<UserUpdated>();
    }

    private const string _email = "email@test.com";
    private const string _name = "UserTest";
}