
namespace Silos.Core.EventBus;

public class IntegrationEvent : IIntegrationEvent
{
    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
}
