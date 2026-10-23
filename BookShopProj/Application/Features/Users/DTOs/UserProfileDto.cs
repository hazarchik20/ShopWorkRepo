using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User
{
    public sealed record UserProfileDto(
        Guid Id,
        string Name,
        string Email,
        string? FirstName,
        string? LastName,
        DateOnly? DateOfBirth,
        string? PhoneNumber,
        string? Town,
        DateTime CreatedAt
    );
}
