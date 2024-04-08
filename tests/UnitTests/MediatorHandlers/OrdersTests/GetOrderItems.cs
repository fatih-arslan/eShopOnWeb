using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;
using NSubstitute;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.MediatorHandlers.OrdersTests;
public class GetOrderItems
{
    private readonly IMapper _mockMapper;
    private readonly IRepository<Order> _mockOrderRepository;
    private readonly OrderGetOrderItemsEndpoint _endpoint;

    public GetOrderItems()
    {
        _mockMapper = Substitute.For<IMapper>();
        _mockOrderRepository = Substitute.For<IRepository<Order>>();
        _endpoint = new OrderGetOrderItemsEndpoint(_mockMapper);
    }

    [Fact]
    public async Task ReturnsOrderItemsForValidOrderId()
    {
        // Arrange
        var orderId = 1;
        var order = new Order(orderId.ToString(), new Address("", "", "", "", ""), new List<OrderItem>()); 
        var orderItemsDto = new OrderItemsDto(); 
        _mockOrderRepository.FirstOrDefaultAsync(Arg.Any<OrderWithItemsByIdSpec>())
            .Returns(Task.FromResult(order));
        _mockMapper.Map<OrderItemsDto>(order).Returns(orderItemsDto);

        // Act
        var result = await _endpoint.HandleAsync(new OrderGetOrderItemsRequest(orderId), _mockOrderRepository);

        // Assert
        var okResult = Assert.IsType<Ok<OrderGetOrderItemsResponse>>(result);
        var response = okResult.Value;
        Assert.NotNull(response.OrderItems);
    }

    [Fact]
    public async Task ReturnsNotFoundForInvalidOrderId()
    {
        // Arrange
        var orderId = 99; 
        _mockOrderRepository.FirstOrDefaultAsync(Arg.Any<OrderWithItemsByIdSpec>())
            .Returns(Task.FromResult<Order>(null));

        // Act
        var result = await _endpoint.HandleAsync(new OrderGetOrderItemsRequest(orderId), _mockOrderRepository);

        // Assert
        Assert.IsType<NotFound<string>>(result);
    }
}
