using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User
{
    public sealed record UserProfileDto(
        Guid Id,
        string? FirstName,
        string? LastName,
        string Email,
        DateOnly? DateOfBirth,
        string? PhoneNumber
    );
}
