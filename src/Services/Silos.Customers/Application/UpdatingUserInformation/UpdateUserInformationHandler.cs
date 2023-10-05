namespace Silos.Users.Application.UpdatingUserInformation;

public class UpdateUserInformationHandler : ICommandHandler<UpdateUserInformation>
{
    private readonly IEventStoreRepository<User> _customerWriteRepository;

    public UpdateUserInformationHandler(
        IEventStoreRepository<User> customerWriteRepository)
    {
        _customerWriteRepository = customerWriteRepository;
    }

    public async Task Handle(UpdateUserInformation command, CancellationToken cancellationToken)
    {
        var customer = await _customerWriteRepository
            .FetchStreamAsync(command.CustomerId.Value);

        if (customer is null)
            throw new ArgumentNullException("User not found.");

        var customerData = new UserData(
            customer.Email,
            command.Name);

        customer.UpdateCustomerInformation(customerData);

        await _customerWriteRepository
            .AppendEventsAsync(customer, cancellationToken);
    }
}

public record UpdateUserRequest(
    string Email,
    string Password,
    string PasswordConfirm);