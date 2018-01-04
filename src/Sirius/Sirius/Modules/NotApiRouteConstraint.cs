using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Sirius.Modules
{
    /// <summary>
    /// Route constraint filtering api requests
    /// </summary>
    public class NotApiRouteConstraint : IRouteConstraint
    {
        #region Implementation of IRouteConstraint

        /// <inheritdoc />
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values,
          RouteDirection routeDirection)
        {
          return !httpContext.Request.Path.StartsWithSegments("/api");
        }

        #endregion
    }
}
