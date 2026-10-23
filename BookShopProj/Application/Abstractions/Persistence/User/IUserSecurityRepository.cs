using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Persistence.User
{
    public interface IUserSecurityRepository : IRepositoryBase<UserSecurity>
    {
        Task<UserSecurity?> GetByUserIdAsync(
            Guid id,
            CancellationToken cancellationToken = default
        );
    }
}
