using Silos.ApiGateway.SignalR.Hubs.Order;

namespace Silos.ApiGateway.SignalR;

[Authorize]
[Route("api/signalr")]
[ApiController]
public class SignalrController : ControllerBase
{
    private IOrderStatusUpdater _orderStatusUpdater;

    public SignalrController(IOrderStatusUpdater orderStatusUpdater)
    {
        _orderStatusUpdater = orderStatusUpdater;
    }

    [HttpPost, Route("updateorderstatus")]
    public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateOrderStatusRequest request)
    {
        await _orderStatusUpdater.UpdateOrderStatus(request.UserId,
            request.OrderId,
            request.OrderStatusText,
            request.OrderStatusCode);

        return Ok();
    }

    public record UpdateOrderStatusRequest(Guid UserId, Guid OrderId, string OrderStatusText, int OrderStatusCode);
}
