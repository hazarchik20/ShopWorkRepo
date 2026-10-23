using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Advert;
namespace Domain.Entities.User
{
    public sealed class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Town { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<Advert.Advert> Adverts { get; set; } = new List<Advert.Advert>();
        public UserSecurity? Security { get; set; }
    }
}
