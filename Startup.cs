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
        
        app.Use(async (context, next)=> {
            await context.Response.WriteAsync(">>>");
            await next();
            await context.Response.WriteAsync("<<<");
        });
       
        //Middleware criado utilizando um arquivo sem método de extensão.
        app.UseMiddleware<MiddlewareAcrescentaChaves>();
        
        //Middleware sendo utilizado utilizando métodos de extensão.
        app.UsarChaves();
 
 

        app.Run( async context =>
        {
            await context.Response.WriteAsync("Middleware Terminal");
        });

        // a saída do programa será =  >>>[[[[[[Middleware Terminal]]]]]]<<< 
    }
}