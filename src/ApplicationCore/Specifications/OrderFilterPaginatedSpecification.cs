using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;
public class OrderFilterPaginatedSpecification : Specification<Order>
{
    public OrderFilterPaginatedSpecification(int skip, int take, string? buyerId) : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query
            .Where(o => (buyerId == null || o.BuyerId == buyerId))
            .Skip(skip).Take(take);
        Query.Include(o => o.OrderItems);

    }
}
