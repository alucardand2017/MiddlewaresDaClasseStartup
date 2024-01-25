public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
              
        //Middleware criado utilizando um arquivo sem método de extensão.
        //app.UseMiddleware<MiddlewareAcrescentaChaves>();
        
        //Middleware sendo utilizado utilizando métodos de extensão.
        app.UsarChaves();
        app.UseWhen( context=>context.Request.Query.ContainsKey("CaminhoC"),
        appC => 
        {
            appC.Use( async (context, next) =>
            {
                await context.Response.WriteAsync("\nRamificacao C");
                await next();
            });
        });

        app.Map("/CaminhoB", async appB =>
        {
            appB.Run( async context => 
            {
                await context.Response.WriteAsync("\nRamificacao B");
            });
        });

         app.Run( async context =>
        {
            await context.Response.WriteAsync("\nMiddleware Terminal");
        });
    }
}