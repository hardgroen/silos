namespace Silos.Users.Domain;

public sealed class UserId : StronglyTypedId<Guid>
{
    public UserId(Guid value) : base(value)
    {
    }

    public static UserId Of(Guid value)
    {
        return new UserId(value);
    }
}
