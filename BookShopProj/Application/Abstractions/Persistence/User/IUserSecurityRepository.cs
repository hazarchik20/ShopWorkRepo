using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Persistence.User
{
    public interface IUserSecurityRepository
    {
        Task<UserSecurity?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default
        );

        Task<UserSecurity?> GetByUserIdAsync(
            Guid id,
            CancellationToken cancellationToken = default
        );

        Task AddAsync(UserSecurity user, CancellationToken cancellationToken = default);
        Task UpdateAsync(UserSecurity user, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
