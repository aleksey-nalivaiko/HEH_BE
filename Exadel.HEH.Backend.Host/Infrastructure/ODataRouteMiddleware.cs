using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Exadel.HEH.Backend.Host.Infrastructure
{
    public class ODataRouteMiddleware
    {
        private readonly IDictionary<string, string[]> _config = new Dictionary<string, string[]>
        {
            ["/odata/Discount"] = new[] { "searchText" },
        };

        private readonly RequestDelegate _next;

        public ODataRouteMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            if (request.Method == "GET" && request.Path.HasValue &&
                _config.TryGetValue(request.Path.Value, out var queryParams))
            {
                foreach (var queryParam in queryParams)
                {
                    if (!request.Query.ContainsKey(queryParam))
                    {
                        request.QueryString = QueryString.Create(queryParam, null);
                    }
                }
            }

            await _next(context);
        }
    }
}