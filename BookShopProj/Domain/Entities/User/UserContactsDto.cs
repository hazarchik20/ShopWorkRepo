using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User
{
    public sealed record UserContactsDto(
        Guid UserId,
        string Name,
        string Email,
        string? Town,
        string? PhoneNumber
    );
}
