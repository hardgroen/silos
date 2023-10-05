using Silos.Users.Domain.Events;

namespace Silos.Users.Infrastructure.Projections;

public class UserDetails
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    
    internal void Apply(UserRegistered registered)
    {
        Id = registered.CustomerId;
        Email = registered.Email;
        Name = registered.Name;
    }

    internal void Apply(UserUpdated updated)
    {
        Name = updated.Name;
    }
}