using System;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderDto
{   
    public string BuyerId { get; set; }
    public string OrderDate { get; set; }
    public string Status { get; set; }
    public decimal Total { get; set; }

}
