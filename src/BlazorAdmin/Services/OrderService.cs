using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorShared;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace BlazorAdmin.Services;

public class OrderService : IOrderService
{
    private readonly HttpService _httpService;
    private readonly ILogger<OrderService> _logger;


    public OrderService(HttpService httpService, ILogger<OrderService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }   

    public async Task<List<Order>> ListPaged(int pageSize, int pageIndex)
    {
        _logger.LogInformation("Fetching orders from API.");
        var orderResponse = await _httpService.HttpGet<PagedOrderResponse>($"orders?pageSize={pageSize}&pageIndex={pageIndex}");
        return orderResponse.Orders;
    }

    public async Task<OrderItems> GetOrderItems(int orderId)
    {
        _logger.LogInformation("Fetching order items from API.");
        var orderResponse = await _httpService.HttpGet<OrderItemsResponse>($"orders/{orderId}/items");
        return orderResponse.OrderItems;
    }

    public async Task<UpdateOrderResponse> UpdateOrder(UpdateOrderRequest request)
    {
        var response = await _httpService.HttpPut<UpdateOrderResponse>($"orders/{request.Id}", request);
        return response;
    }
}
