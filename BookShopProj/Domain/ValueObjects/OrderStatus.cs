using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public enum OrderStatus
    {
        Processing = 0,
        Confirmed = 1,
        Shipped = 2,
        Delivered = 3,
        Cancelled = 4
    }

}
