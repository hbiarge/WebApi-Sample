using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.IntegrationTests.Infrastructure
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task IsSuccessStatusCodeOrTrow(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var content = await response.Content.ReadAsStringAsync();

            throw new Exception($"Response status does not indicate success: {response.StatusCode:D} ({response.StatusCode}); \r\n{content}");
        }
    }
}
