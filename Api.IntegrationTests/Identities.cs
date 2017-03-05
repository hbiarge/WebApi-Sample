using System.Collections.Generic;
using System.Security.Claims;

namespace Api.IntegrationTests
{
    public static class Identities
    {
        public static IEnumerable<Claim> User = new[]
        {
            new Claim(ClaimTypes.Name, "Hugo") 
        };
    }
}
