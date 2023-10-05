namespace Silos.Users.Infrastructure.Projections;

public static class ProjectionsConfiguration
{
    internal static void ConfigureProjections(this StoreOptions options)
    {
        options.Projections.Add<UserDetailsProjection>(ProjectionLifecycle.Inline);
        options.Projections.Add<UserHistoryTransform>(ProjectionLifecycle.Inline);
    }
}