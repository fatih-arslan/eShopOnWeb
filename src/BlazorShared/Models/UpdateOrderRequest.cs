using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Models;
public class UpdateOrderRequest
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }

}
