using System;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderGetOrderItemsResponse : BaseResponse
{
    public OrderGetOrderItemsResponse(Guid correlationId) : base(correlationId)
    {
        
    }

    public OrderGetOrderItemsResponse()
    {
        
    }

    public OrderItemsDto OrderItems { get; set; }
}
