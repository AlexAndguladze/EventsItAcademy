using EventsItAcademy.API.Infrastructure.Middlewares;

namespace EventsItAcademy.API.Infrastructure.Extensions
{
    public static class CultureMiddlewareExtension
    {
        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CultureMiddleware>();
        }
    }
}
