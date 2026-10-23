using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User
{
    public sealed class UserSecurity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string RegistrationIp { get; set; } = null!;
        public string RegistrationUserAgent { get; set; } = null!;

        public DateTime? LastLogin { get; set; } = null;

        public User User { get; set; } = null!;

    }
}
