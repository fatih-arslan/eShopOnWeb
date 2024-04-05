namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderListPagedRequest : BaseRequest
{
    public OrderListPagedRequest(int pageSize, int pageIndex, string? buyerId)
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
        BuyerId = buyerId;
    }

    public int PageSize { get; init; }
    public int PageIndex { get; init; }
    public string? BuyerId { get; set; }
}
