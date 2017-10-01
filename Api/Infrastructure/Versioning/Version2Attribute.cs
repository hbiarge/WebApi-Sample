using System;
using Microsoft.Web.Http;

namespace Api.Infrastructure.Versioning
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class Version2Attribute : ApiVersionAttribute
    {
        public Version2Attribute() : base(new ApiVersion(2, 0))
        {
            Deprecated = false;
        }
    }
}