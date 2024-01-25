using Microsoft.AspNetCore.Builder;

public static class MetodosExtensao
{
    public static IApplicationBuilder UsarChaves(this IApplicationBuilder app)
    {
        app.UseMiddleware<MiddlewareTempoExecucao>();
        return app;

    }
}