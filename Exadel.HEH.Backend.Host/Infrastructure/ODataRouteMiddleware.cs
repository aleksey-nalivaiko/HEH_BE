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
            ["/odata/Vendor"] = new[] { "searchText" },
            ["/odata/Statistics"] = new[] { "searchText", "startDate", "endDate" },
            ["/odata/Favorites"] = new[] { "searchText" }
        };

        private readonly IDictionary<string, string> _lowerCaseRoutes = new Dictionary<string, string>
        {
            ["/odata/discountCreateUpdate"] = "/odata/Discount",
            ["/odata/user"] = "/odata/User",
            ["/odata/vendor"] = "/odata/Vendor",
            ["/odata/statistics"] = "/odata/Statistics",
            ["/odata/favorites"] = "/odata/Favorites",
            ["/odata/history"] = "/odata/History"
        };

        private readonly RequestDelegate _next;

        public ODataRouteMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;

            if (request.Method == "GET" && request.Path.HasValue)
            {
                foreach (var lowerCaseRoute in _lowerCaseRoutes)
                {
                    var requestString = request.Path.ToString();

                    if (requestString.Contains(lowerCaseRoute.Key))
                    {
                        request.Path = requestString.Replace(lowerCaseRoute.Key, lowerCaseRoute.Value);
                        break;
                    }
                }

                if (_config.TryGetValue(request.Path.Value, out var queryParams))
                {
                    foreach (var queryParam in queryParams)
                    {
                        if (!request.Query.ContainsKey(queryParam))
                        {
                            request.QueryString = request.QueryString.Add(QueryString.Create(queryParam, null));
                        }
                    }
                }
            }

            await _next(context);
        }
    }
}