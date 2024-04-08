using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Specifications;
public class OrderFilterPaginatedSpecificationTests
{
    [Fact]
    public void AppliesFilterAndPaginationCorrectly()
    {
        // Arrange
        var spec = new OrderFilterPaginatedSpecification(1, 2, "1");
        var expectedCount = 2;
        var expectedStreet = "street2";

        // Act
        var result = spec.Evaluate(GetTestOrderCollection()).ToList();

        // Assert
        Assert.Equal(result.Count, expectedCount);
        Assert.Equal(result[0].ShipToAddress.Street, expectedStreet);   
    }

    [Fact]
    public void ReturnsAllOrdersWhenSkipAndTakeIsZero()
    {
        // Arrange
        var spec = new OrderFilterPaginatedSpecification(0, 0, "1");
        var expectedCount = 4;

        // Act
        var result = spec.Evaluate(GetTestOrderCollection()).ToList();

        // Assert
        Assert.Equal(result.Count, expectedCount);
    }

    private List<Order> GetTestOrderCollection()
    {
        return new List<Order>
        {
            new Order("1", new Address("street1", "city1", "state1", "country1", "zip1"), new List<OrderItem>()),
            new Order("1", new Address("street2", "city2", "state2", "country2", "zip2"), new List<OrderItem>()),
            new Order("1", new Address("street3", "city3", "state3", "country3", "zip3"), new List<OrderItem>()),
            new Order("1", new Address("street4", "city4", "state4", "country4", "zip4"), new List<OrderItem>()),
            new Order("2", new Address("street5", "city5", "state5", "country5", "zip5"), new List<OrderItem>()),
            new Order("3", new Address("street6", "city6", "state6", "country6", "zip6"), new List<OrderItem>()),
        };
    }

}

