namespace Silos.Core.Infrastructure.SignalR;

public record UpdateOrderStatusRequest(
    Guid UserId, 
    Guid OrderId, 
    string OrderStatusText, 
    int OrderStatusCode);