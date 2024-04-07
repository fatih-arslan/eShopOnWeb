using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorShared.Models;
public class OrderItems
{
    public int OrderId { get; set; }

    [JsonPropertyName("orderItems")]
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}
