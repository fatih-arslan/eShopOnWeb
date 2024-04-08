using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorAdmin.Pages.OrderPage;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Specifications;
public class OrderFilterSpecificationTests
{
    [Fact]
    public void MatchesExpectedNumberOfOrders()
    {       
        // Arrange
        var spec = new OrderFilterSpecification("1");
        var expectedCount = 2;

        // Act
        var result = spec.Evaluate(GetTestOrderCollection()).ToList();


        // Assert
        Assert.Equal(expectedCount, result.Count());
    }

    [Fact]
    public void ReturnsAllOrdersWithNullBuyerId()
    {
        // Arrange
        var spec = new OrderFilterSpecification(null);
        var expectedCount = 4;

        // Act
        var result = spec.Evaluate(GetTestOrderCollection()).ToList();

        // Assert
        Assert.Equal(expectedCount, result.Count());
    }
    private List<Order> GetTestOrderCollection()
    {
        return new List<Order>
        {
            new Order("1", new Address("street", "city", "state", "country", "zip"), new List<OrderItem>()),
            new Order("1", new Address("street", "city", "state", "country", "zip"), new List<OrderItem>()),
            new Order("2", new Address("street", "city", "state", "country", "zip"), new List<OrderItem>()),
            new Order("3", new Address("street", "city", "state", "country", "zip"), new List<OrderItem>()),
        };
    }
}
