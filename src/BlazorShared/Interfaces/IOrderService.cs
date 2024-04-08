using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorShared.Models;

namespace BlazorShared.Interfaces;
public interface IOrderService
{
    Task<List<Order>> ListPaged(int pageSize, int pageIndex);
    Task<OrderItems> GetOrderItems(int orderId);
    Task<UpdateOrderResponse> UpdateOrder(UpdateOrderRequest request);
}
