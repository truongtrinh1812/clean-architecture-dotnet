using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Infra.Authentication
{
    public interface ISecurityContextAccessor
    {
        string UserId { get; }
        string Role { get; }
        string JwtToken { get; }
        bool IsAuthenticated { get; }
    }
}
