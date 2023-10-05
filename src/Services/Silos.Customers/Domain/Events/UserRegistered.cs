namespace Silos.Users.Domain.Events;

public record class UserRegistered : IDomainEvent
{
    public Guid CustomerId { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }

    public static UserRegistered Create(
        Guid customerId,
        string name,
        string email)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentOutOfRangeException(nameof(customerId));
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException(nameof(email));
        
        return new UserRegistered(
            customerId,
            name,
            email);
    }

    private UserRegistered(
        Guid customerId,
        string name,
        string email)
    {
        CustomerId = customerId;
        Name = name;
        Email = email;
    }
}