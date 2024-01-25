using Microsoft.Extensions.Options;

public class Startup
{
    private IConfiguration configuration { get; }
    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
       services.Configure<ContadorOptions>(opt =>
       {
            opt.Quantidade = 5;
       });

        services.Configure<CronometroOptions>(opt =>
       {
            opt.UnidadeMedida = UnidadesTempo.Microssegundo;
       });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<ContadorOptions> opt)
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

        app.Map("/CaminhoB", appB => 
        {
            appB.Run( async context => 
            {
                await context.Response.WriteAsync("\nRamificacao B");
            });
        });

        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("===");
            await next();
            await context.Response.WriteAsync("===");
        });
                app.Use(async (context, next) =>
        {
            var ContadorOptions = opt.Value;
            await context.Response.WriteAsync(new string ('>', ContadorOptions.Quantidade));
            await next();
            await context.Response.WriteAsync(new string ('<', ContadorOptions.Quantidade));
        });

         app.Run( async context =>
        {
            await context.Response.WriteAsync("Middleware Terminal");
        });
    }
}