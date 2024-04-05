using System.Collections.Generic;
using System.Threading.Tasks;
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

    public async Task<List<Order>> ListPaged()
    {
        _logger.LogInformation("Fetching orders from API.");
        var orderResponse = await _httpService.HttpGet<PagedOrderResponse>($"orders?PageSize={10}&pageIndex={0}");
        return orderResponse.Orders;
    }
}
