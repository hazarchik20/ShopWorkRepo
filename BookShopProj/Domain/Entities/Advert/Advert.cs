using Domain.Entities.User;
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

        public string? Town { get; set; }

        public decimal Price { get; set; }
        public bool IsPriceNegotiable { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public AdvertStateOfUse StateOfUse { get; set; }
        public AdvertDealType AdvertDealType { get; set; }
        public AdvertSellerType SellerType { get; set; }
        public AdvertStatus Status { get; set; } = AdvertStatus.Active;

        public List<AdvertGenres> Genres { get; set; } = new();
        public List<string> ImagesLinks { get; set; } = new();

        public User.User User { get; set; } = null!;
    }

}
