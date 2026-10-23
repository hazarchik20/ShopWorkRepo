using Domain.ValueObjects.Advart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Advert
{
     public sealed record UsersAdvert(
        Guid Id,
        string Title,
        decimal Price,
        DateTime CreatedAt,
        AdvertStateOfUse State,
        //bool IsActive,
        int ViewsCount ,
        int DesiredCountQ
     );
}
