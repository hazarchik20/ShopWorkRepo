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
        public string RegistractionUserAgent { get; set; } = null!;

        public DateTime? LastLogin { get; set; } = null;

    }
}
