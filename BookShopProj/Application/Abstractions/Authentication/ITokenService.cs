using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Authentication
{
    public interface ITokenService
    {
        string GenerateAccessToken(Guid userId,string email);
    }
}
