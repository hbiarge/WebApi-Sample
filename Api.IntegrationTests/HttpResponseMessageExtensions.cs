using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Api.IntegrationTests
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
