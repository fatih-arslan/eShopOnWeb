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
public class ListPaged
{
    private readonly IRepository<Order> _mockOrderRepository;
    private readonly IMapper _mockMapper;

    public ListPaged()
    {
        _mockOrderRepository = Substitute.For<IRepository<Order>>();
        _mockMapper = Substitute.For<IMapper>();
    }

    [Fact]
    public async Task ReturnsPagedOrdersSuccessfully()
    {
        // Arrange
        var request = new OrderListPagedRequest(10, 0, null);
        var orderList = new List<Order>
            {
                new Order("1", new Address("", "", "", "", ""), new List<OrderItem>()),
                new Order("2", new Address("", "", "", "", ""), new List<OrderItem>())
            };
        _mockOrderRepository.CountAsync(Arg.Any<OrderFilterSpecification>()).Returns(20);
        _mockOrderRepository.ListAsync(Arg.Any<OrderFilterPaginatedSpecification>())
            .Returns(Task.FromResult(orderList));
        _mockMapper.Map<OrderDto>(Arg.Any<Order>()).Returns(new OrderDto());

        var endpoint = new OrderListPagedEndpoint(_mockMapper);

        // Act
        var result = await endpoint.HandleAsync(request, _mockOrderRepository);

        // Assert
        var okResult = Assert.IsType<Ok<OrderListPagedResponse>>(result);
        var response = okResult.Value;
        Assert.Equal(2, response.Orders.Count);
        Assert.Equal(2, response.PageCount);
    }

    [Fact]
    public async Task HandlesNoOrdersFound()
    {
        // Arrange
        var request = new OrderListPagedRequest(10, 0, "NonExistentBuyerId");
        _mockOrderRepository.CountAsync(Arg.Any<OrderFilterSpecification>()).Returns(0);
        _mockOrderRepository.ListAsync(Arg.Any<OrderFilterPaginatedSpecification>())
        .Returns(Task.FromResult(new List<Order>()));

        var endpoint = new OrderListPagedEndpoint(_mockMapper);

        // Act
        var result = await endpoint.HandleAsync(request, _mockOrderRepository);

        // Assert
        var okResult = Assert.IsType<Ok<OrderListPagedResponse>>(result);
        var response = okResult.Value;
        Assert.Empty(response.Orders);
        Assert.Equal(0, response.PageCount);
    }

    [Fact]
    public async Task ValidatesMappingOfOrders()
    {
        // Arrange
        var orderList = new List<Order> 
            { 
                new Order("1", new Address("", "", "", "", ""), new List<OrderItem>()), 
                new Order("2", new Address("", "", "", "", ""), new List<OrderItem>()) 
            };
        _mockOrderRepository.ListAsync(Arg.Any<OrderFilterPaginatedSpecification>()).Returns(Task.FromResult(orderList));
        _mockMapper.Map<OrderDto>(Arg.Any<Order>()).Returns(new OrderDto(), new OrderDto()); // Simulate mapping to different DTOs

        var request = new OrderListPagedRequest(10, 0, null);
        var endpoint = new OrderListPagedEndpoint(_mockMapper);

        // Act
        await endpoint.HandleAsync(request, _mockOrderRepository);

        // Assert
        _mockMapper.Received(orderList.Count).Map<OrderDto>(Arg.Any<Order>());
    }
}
