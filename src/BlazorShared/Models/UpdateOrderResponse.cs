using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Models;
public class UpdateOrderResponse
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}
