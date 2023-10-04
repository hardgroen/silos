namespace Silos.Core.Testing;

public sealed class DummyIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; set; }
    public string Text { get; set; }
}