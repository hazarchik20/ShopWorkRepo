using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Storage
{

    public sealed record AdvertUpload(
        Stream Content,
        string ContentType,
        long Length
    );

}
