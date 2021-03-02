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
            ["/odata/Statistics/Excel"] = new[] { "searchText", "startDate", "endDate" },
            ["/odata/Statistics/Excel2"] = new[] { "searchText", "startDate", "endDate" },
            ["/odata/Favorites"] = new[] { "searchText" }
        };

        private readonly IDictionary<string, string> _lowerCaseRoutes = new Dictionary<string, string>
        {
            ["/odata/discount"] = "/odata/Discount",
            ["/odata/user"] = "/odata/User",
            ["/odata/vendor"] = "/odata/Vendor",
            ["/odata/statistics"] = "/odata/Statistics",
            ["/odata/statistics/excel"] = "/odata/Statistics/Excel",
            ["/odata/favorites"] = "/odata/Favorites",
            ["/odata/history"] = "/odata/History",
            ["/odata/notification"] = "/odata/Notification"
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
                if (_lowerCaseRoutes.TryGetValue(request.Path.Value, out var lowerCaseRoute))
                {
                    request.Path = request.Path.Value.Replace(request.Path.Value, lowerCaseRoute);
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