using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;
using NSubstitute;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.MediatorHandlers.OrdersTests;
public class UpdateOrder
{
    private readonly IRepository<Order> _mockOrderRepository = Substitute.For<IRepository<Order>>();

    [Fact]
    public async Task HandleAsync_ExistingOrder_UpdatesSuccessfully()
    {
        // Arrange
        var request = new UpdateOrderRequest { Id = 1, Status = OrderStatus.Approved };
        var order = new Order("1", new Address("", "", "", "", ""), new List<OrderItem>());
        _mockOrderRepository.GetByIdAsync(request.Id).Returns(Task.FromResult(order));

        var endpoint = new UpdateOrderEndpoint();

        // Act
        var result = await endpoint.HandleAsync(request, _mockOrderRepository);

        // Assert
        var okResult = Assert.IsType<Ok<UpdateOrderResponse>>(result);
        var response = Assert.IsType<UpdateOrderResponse>(okResult.Value);
        Assert.True(response.Success);
    }

    [Fact]
    public async Task HandleAsync_OrderNotFound_ReturnsNotFound()
    {
        // Arrange
        var request = new UpdateOrderRequest { Id = 1, Status = OrderStatus.Approved }; // Assuming this ID does not exist
        _mockOrderRepository.GetByIdAsync(request.Id).Returns(Task.FromResult<Order>(null));

        var endpoint = new UpdateOrderEndpoint();

        // Act
        var result = await endpoint.HandleAsync(request, _mockOrderRepository);

        // Assert
        var notFoundResult = Assert.IsType<NotFound<string>>(result);
        Assert.NotNull(notFoundResult.Value); 
    }   
}
