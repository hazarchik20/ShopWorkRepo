using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Persistence.User
{
    public interface IUserRepository
    {
        Task<Domain.Entities.User.User?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default
        );

        Task<IEnumerable<Domain.Entities.User.User>> GetPagedAsync(
            int page, 
            int pageSize, 
            CancellationToken cancellationToken = default
        );

        Task<Domain.Entities.User.User?> GetByEmailAsync(
            string email,
            CancellationToken cancellationToken = default
        );

        Task<bool> ExistsByEmailAsync(
            string email,
            CancellationToken cancellationToken = default
        );

        Task AddAsync(Domain.Entities.User.User user, CancellationToken cancellationToken = default);
        Task UpdateAsync(Domain.Entities.User.User user, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
