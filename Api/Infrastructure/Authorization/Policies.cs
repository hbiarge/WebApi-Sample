using Microsoft.Owin.Security.Authorization;

namespace Api.Infrastructure.Authorization
{
    public class Policies
    {
        public const string Administrator = "Administrator";
        public const string Vendor = "Vendor";

        public static void Configure(AuthorizationOptions options)
        {
            options.AddPolicy(
                Administrator,
                policy => policy.RequireRole("Administrator"));

            options.AddPolicy(
                Vendor,
                policy => policy.RequireRole("Vendor"));
        }
    }
}
