using System;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class UpdateOrderResponse : BaseResponse
{
    public UpdateOrderResponse(Guid correlationId) : base(correlationId)
    {
        
    }

    public UpdateOrderResponse()
    {
        
    }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}
