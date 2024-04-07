namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderItemDto
{
    public string ProductName { get; set; }
    public int Units { get; set; }
    public decimal Total { get; set; }
}
