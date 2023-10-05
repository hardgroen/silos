using Silos.Users.Domain.Events;

namespace Silos.Users.Domain;

public sealed class User : AggregateRoot<UserId>
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    
    public static User Create(UserData customerData)
    {
        var (Email, Name) = customerData
            ?? throw new ArgumentNullException(nameof(customerData));

        if (string.IsNullOrWhiteSpace(Email))
            throw new BusinessRuleException("Customer email cannot be null or whitespace.");

        if (string.IsNullOrWhiteSpace(Name))
            throw new BusinessRuleException("Customer name cannot be null or whitespace.");

        return new User(customerData);
    }

    public void UpdateCustomerInformation(UserData customerData)
    {
        var (Email, Name) = customerData
            ?? throw new ArgumentNullException(nameof(customerData));

        if (string.IsNullOrWhiteSpace(customerData.Name))
            throw new BusinessRuleException("Customer name cannot be null or whitespace.");

        var @event = UserUpdated.Create(
            Id.Value,
            Name);

        AppendEvent(@event);
        Apply(@event);
    }

    private void Apply(UserRegistered registered)
    {
        Id = UserId.Of(registered.CustomerId);
        Email = registered.Email;
        Name = registered.Name;
    }

    private void Apply(UserUpdated updated)
    {
        Name = updated.Name;
    }

    private User(UserData customerData)
    {
        var @event = UserRegistered.Create(
            Guid.NewGuid(),
            customerData.Name,
            customerData.Email);

        AppendEvent(@event);
        Apply(@event);
    }

    private User() { }
}