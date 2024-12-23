public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews(); 

        services.AddScoped<OrderRepository>();
        services.AddScoped<SparePartRepository>();
        services.AddScoped<WorkRepository>();
        services.AddScoped<MalfunctionRepository>();
        services.AddScoped<MasterRepository>();
        services.AddScoped<OrderSparePartRepository>();
        services.AddScoped<OrderMalfunctionRepository>();
        services.AddScoped<OrderWorkRepository>();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                context.Response.Redirect("/Order/Index");
            });

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "Order/{action=Index}/{id?}",
                defaults: new { controller = "Order" }
            );
        });
    }
}