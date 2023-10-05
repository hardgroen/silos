using Silos.Users.Domain;

namespace Silos.Customers.Tests.Domain;

public class CustomerCreationTests
{
    [Fact]
    public void CreatingCustomer_WithCustomerData_ReturnsCustomer()
    {
        // Given
        string email = "email@test.com";
        string name = "UserTest";

        var customerData = new UserData(email, name);

        // When
        var customer = User.Create(customerData);

        // Then
        Assert.NotNull(customer);
        customer.Id.Value.Should().NotBe(Guid.Empty);
        customer.Email.Should().Be(email);
        customer.Name.Should().Be(name);
    }
}