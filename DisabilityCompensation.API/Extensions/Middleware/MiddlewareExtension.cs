﻿namespace DisabilityCompensation.API.Extensions.Middleware
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder AddMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ValidationMiddleware>();
        }
    }
}