using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Persistence.User
{
    public interface IUserRepository : IRepositoryBase<Domain.Entities.User.User>
    {
        Task<Domain.Entities.User.User?> GetByEmailAsync(
            string email,
            CancellationToken cancellationToken = default
        );

        Task<Domain.Entities.User.User?> GetByGoogleSubjectAsync(
            string googleSubject,
            CancellationToken cancellationToken = default
        );

        Task<bool> ExistsByEmailAsync(
            string email,
            CancellationToken cancellationToken = default
        );
    }
}
