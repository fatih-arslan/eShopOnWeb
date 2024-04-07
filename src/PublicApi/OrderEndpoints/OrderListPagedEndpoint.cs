using System;
using System.Collections.Generic;
using System.Linq;
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

public class OrderListPagedEndpoint : IEndpoint<IResult, OrderListPagedRequest, IRepository<Order>>
{
    private readonly IMapper _mapper;

    public OrderListPagedEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/orders",
            async (int pageSize, int pageIndex, string? buyerId, IRepository<Order> orderRepository) =>
            {
                return await HandleAsync(new OrderListPagedRequest(pageSize, pageIndex, buyerId), orderRepository);
            })
            .Produces<OrderListPagedResponse>()
            .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(OrderListPagedRequest request, IRepository<Order> orderRepository)
    {
        await Task.Delay(1000);
        var response = new OrderListPagedResponse(request.CorrelationId());

        var filterSpec = new OrderFilterSpecification(request.BuyerId);
        int totalItems = await orderRepository.CountAsync(filterSpec);

        var pagedSpec = new OrderFilterPaginatedSpecification(
            skip: request.PageIndex * request.PageSize,
            take: request.PageSize,
            buyerId: request.BuyerId);

        var orders = await orderRepository.ListAsync(pagedSpec);

        response.Orders.AddRange(orders.Select(_mapper.Map<OrderDto>));

        if (request.PageSize > 0)
        {
            response.PageCount = int.Parse(Math.Ceiling((decimal)totalItems / request.PageSize).ToString());
        }
        else
        {
            response.PageCount = totalItems > 0 ? 1 : 0;
        }

        return Results.Ok(response);
    }   
}
