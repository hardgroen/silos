namespace Silos.Core.EventBus;

public interface IIntegrationEvent : INotification {
    public Guid Id { get; set; }
}
