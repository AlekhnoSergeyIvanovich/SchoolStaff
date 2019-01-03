using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Controllers;

namespace Presentation.Middleware
{
    public class CustomMiddleware<TDbContext> where TDbContext : DbContext
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Movement
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        // IMyScopedService is injected into Invoke
        public async Task Invoke(HttpContext httpContext, TDbContext context)
        {
            try
            {
                await _next(httpContext);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //var i = e.HResult;
                Console.WriteLine(e);
                throw new ApplicationException("KyKy");
            }
        }
    }

    /// <summary>
    /// Create Middleware
    /// </summary>
    public static class ContextAsCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseContextAsCustomMiddleware<TDbContext>(this IApplicationBuilder builder) where TDbContext : DbContext
        {
            return builder.UseMiddleware<CustomMiddleware<TDbContext>>();
        }
    }
}