using Silos.Users.Domain;

namespace Silos.Users.Domain.Commands;

public record class UpdateUserInformation : ICommand
{
    public UserId CustomerId { get; private set; }
    public string Name { get; private set; }
    
    public static UpdateUserInformation Create(
        UserId customerId,
        string name)
    {
        if (customerId is null)
            throw new ArgumentNullException(nameof(customerId));
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        
        return new UpdateUserInformation(
            customerId,
            name);
    }

    private UpdateUserInformation(
        UserId customerId,
        string name)
    {
        CustomerId = customerId;
        Name = name;
    }
}
