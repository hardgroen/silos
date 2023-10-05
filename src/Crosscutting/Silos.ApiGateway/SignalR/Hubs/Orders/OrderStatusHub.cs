namespace Silos.ApiGateway.SignalR.Hubs.Order;

public interface IOrderStatusHubClient
{
    Task UpdateOrderStatus(string orderId, string orderStatusText, int orderStatusCode);
}

public class OrderStatusHub : Hub<IOrderStatusHubClient>
{
    public async Task JoinUserToGroup(string userId) =>    
        await Groups.AddToGroupAsync(Context.ConnectionId, userId);

}