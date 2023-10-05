namespace Silos.Users.Domain.Events;

public record class UserUpdated : IDomainEvent
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }

    public static UserUpdated Create(
        Guid customerId,
        string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));        

        return new UserUpdated(
            customerId,
            name);
    }

    private UserUpdated(
        Guid customerId,
        string name)
    {
        UserId = customerId;
        Name = name;
    }
}