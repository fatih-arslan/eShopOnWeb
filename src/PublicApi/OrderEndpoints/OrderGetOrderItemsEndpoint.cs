using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderGetOrderItemsEndpoint : IEndpoint<IResult, OrderGetOrderItemsRequest, IRepository<Order>>
{
    private readonly IMapper _mapper;

    public OrderGetOrderItemsEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/orders/{orderId}/items",
           async (int orderId, IRepository<Order> orderRepository) =>
           {
               return await HandleAsync(new OrderGetOrderItemsRequest(orderId), orderRepository);
           })
           .Produces<OrderGetOrderItemsResponse>()
           .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(OrderGetOrderItemsRequest request, IRepository<Order> orderRepository)
    {
        var response = new OrderGetOrderItemsResponse(request.CorrelationId());

        var filterSpec = new OrderWithItemsByIdSpec(request.OrderId);

        var order = await orderRepository.FirstOrDefaultAsync(filterSpec);
        
        if(order == null)
        {
            return Results.NotFound($"Order with ID {request.OrderId} not found.");
        }

        response.OrderItems = _mapper.Map<OrderItemsDto>(order);
        
        return Results.Ok(response);
    }
}
