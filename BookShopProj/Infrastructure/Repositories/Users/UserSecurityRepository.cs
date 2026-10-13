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
    public sealed class UserSecurityRepository : RepositoryBase<UserSecurity>, IUserSecurityRepository
    {
        public UserSecurityRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UserSecurity?> GetByUserIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(us => us.UserId == id, cancellationToken);
        }
    }
}
