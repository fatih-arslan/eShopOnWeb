using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared;
public enum OrderStatus
{
    Pending,
    Approved,
    CanceledByClient,
    CanceledByAdmin
}
