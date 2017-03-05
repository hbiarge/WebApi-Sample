using System.Collections.Generic;
using System.Security.Claims;

namespace Api.IntegrationTests
{
    public static class Identities
    {
        public static readonly IEnumerable<Claim> Administrator = new[]
        {
            new Claim(ClaimTypes.Role, "Administrator"),
            new Claim(ClaimTypes.Name, "Hugo")
        };

        public static readonly IEnumerable<Claim> User = new[]
        {
            new Claim(ClaimTypes.Role, "Vendor"),
            new Claim(ClaimTypes.Name, "Hugo") 
        };
    }
}
