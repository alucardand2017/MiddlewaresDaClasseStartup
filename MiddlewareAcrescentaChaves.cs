using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class MiddlewareAcrescentaChaves
{
    private readonly RequestDelegate _next;


public MiddlewareAcrescentaChaves(RequestDelegate next)
{
    _next = next;
}

public async Task Invoke(HttpContext context)
{
    await context.Response.WriteAsync("\n[[[");
    await _next(context);
    await context.Response.WriteAsync("\n]]]");
}

}