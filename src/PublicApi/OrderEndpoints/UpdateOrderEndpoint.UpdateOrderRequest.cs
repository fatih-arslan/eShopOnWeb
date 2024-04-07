using System.Text.Json;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class UpdateOrderRequest : BaseRequest
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }

}
