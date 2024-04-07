using System;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderListPagedResponse : BaseResponse
{
    public OrderListPagedResponse(Guid correlationId) : base(correlationId)
    {
        
    }

    public OrderListPagedResponse()
    {
        
    }

    public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
    public int PageCount { get; set; }
}
