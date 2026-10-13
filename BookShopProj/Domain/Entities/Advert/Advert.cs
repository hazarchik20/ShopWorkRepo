using Domain.ValueObjects.Advart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Advert
{
    public sealed class Advert
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public bool isPriceNegotiable { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public AdvartStateOfUse StateOfUse { get; set; }
        public AdvartMethodOfAgreement MethodOfAgreement { get; set; }
        public AdvartSellerType SellerType { get; set; }

        public List<AdvartGenres> Genres { get; set; } = new List<AdvartGenres>();
        public List<AdvertImage> Images { get; set; } = new List<AdvertImage>();    

    }
}
