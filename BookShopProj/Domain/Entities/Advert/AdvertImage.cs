using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Advert
{
    public sealed class AdvertImage
    {
        public Guid Id { get; set; }
        public Guid AdvertId { get; set; }

        public byte[] ImageData { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public int Order { get; set; }

        public Advert Advert { get; set; } = null!;
    }
}
