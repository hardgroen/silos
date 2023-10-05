using Silos.Users.Domain;
using Silos.Users.Application.UpdatingUserInformation;

namespace Silos.Customers.Tests.Application;

public class UpdateCustomerInformationHandlerTests
{
    [Fact]
    public async Task UpdateCustomerInformation_WithCommand_ShouldUpdateCustomerInformation()
    {
        // Given
        string email = "email@test.com";
        string name = "UserTest";

        var customerWriteRepository = new DummyEventStoreRepository<User>();
        var customerData = new UserData(email, name);
        var customer = User.Create(customerData);
        await customerWriteRepository.AppendEventsAsync(customer);

        var newName = "New Name";
        var updateCommand = UpdateUserInformation.Create(customer.Id, newName);
        var commandHandler = new UpdateUserInformationHandler(customerWriteRepository);

        // When
        await commandHandler.Handle(updateCommand, CancellationToken.None);
        var updatedCustomer = await customerWriteRepository.FetchStreamAsync(customer.Id.Value);

        // Then
        updatedCustomer.Name.Should().Be(newName);
    }
}