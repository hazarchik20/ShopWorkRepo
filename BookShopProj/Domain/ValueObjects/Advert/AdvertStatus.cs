using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects.Advart
{

    public enum AdvertStatus
    {
        Review = 0,
        Active = 1,
        Reserved = 2,
        Sold = 3,
        Hidden = 4,
        Rejected = 5,
        Deleted = 6
    }

}
