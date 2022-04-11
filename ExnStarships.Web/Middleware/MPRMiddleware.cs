using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace ExnStarships.Web.Middleware;

/// <summary>
/// In accordance to the Martian Privacy Regulations, requests coming from Mars must contain a special header  
/// </summary>
public class MPRMiddleware
{
    private readonly RequestDelegate next;

    readonly string[] targetUserAgent = { "Mars", "MARS" , "UnitedMartianFederation", "UMF" };
    const string headerKey = "Martian-Privacy-Regulation";
    const string headerValue = "RESPECTED";

    public MPRMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
        StringValues userAgent = httpContext.Request.Headers.UserAgent;

        foreach (var s in targetUserAgent)
        {
            if (!userAgent.Contains(s)) continue;

            httpContext.Response.Headers.Add(headerKey, headerValue);
            break;
        }

        return next(httpContext);
    }
}

public static class MPRMiddlewareExtensions
{
    public static IApplicationBuilder UseMPR(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MPRMiddleware>();
    }
}
