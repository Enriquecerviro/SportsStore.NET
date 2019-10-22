using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infraestructure
{
    /// <summary>
    /// The PathAndQuery extension method operates on the HttpRequest class, which ASP.NET uses to 
    /// describe an HTTP request.The extension method generates a URL that the browser will be returned to after 
    /// the cart has been updated, taking into account the query string if there is one
    /// </summary>
    public static class UrlExtensions
    {
        /// <summary>
        /// Pathes the and query.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static string PathAndQuery(this HttpRequest request) =>
            request.QueryString.HasValue
            ? $"{request.Path}{request.QueryString}"
            : request.Path.ToString();
    }
}
