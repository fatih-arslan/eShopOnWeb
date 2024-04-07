namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderGetOrderItemsRequest : BaseRequest
{
    public int OrderId { get; init; }

    public OrderGetOrderItemsRequest(int orderId)
    {
        OrderId = orderId;
    }
}
