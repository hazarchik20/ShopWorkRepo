using Microsoft.EntityFrameworkCore;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Persistence.User;

namespace Infrastructure.Repositories.Users
{

    public sealed class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<User?> GetByGoogleSubjectAsync(
            string googleSubject,
            CancellationToken cancellationToken = default)
        {
            return _dbSet.FirstOrDefaultAsync(
                x => x.GoogleSubject == googleSubject,
                cancellationToken);
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            email = email.Trim().ToLowerInvariant();

            return await _dbSet.AnyAsync(
                x => x.Email == email,
                cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            email = email.Trim().ToLowerInvariant();

            return await _dbSet.FirstOrDefaultAsync(
                x => x.Email == email,
                cancellationToken);
        }
    }

}
