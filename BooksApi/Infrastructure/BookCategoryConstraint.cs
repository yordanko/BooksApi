﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Infrastructure
{
    public class BookCategoryConstraint : IRouteConstraint
    {
        public static string[] Categories = new[] { "all", "computers", "cooking" };
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return Categories.Contains(values[routeKey]?.ToString().ToLowerInvariant());
        }
    }
}
