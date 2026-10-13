using Application.Abstractions.Persistence.User;
using Domain.Entities.Advert;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Advart
{
    public sealed class AdvartRepository : RepositoryBase<Advert>, IUserProfileRepository<Advert>
    {
        public AdvartRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
    }
}
