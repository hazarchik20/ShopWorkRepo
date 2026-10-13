using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects.User
{
    public static class UserNameGenerator
    {
        public static string Generate()
        {
            return $"User{Guid.NewGuid():N}"[..16];
        }
    }
}
