using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;
public class OrderFilterSpecification : Specification<Order>
{
    public OrderFilterSpecification(string? buyerId)
    {
        Query
            .Where(o => (buyerId == null || o.BuyerId == buyerId));
    }
}
