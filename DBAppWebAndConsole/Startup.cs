public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews(); // Добавляем поддержку MVC и представлений.

        // Добавляем репозитории для внедрения зависимостей
        services.AddScoped<OrderRepository>();
        services.AddScoped<SparePartRepository>();
        services.AddScoped<WorkRepository>();
        services.AddScoped<MalfunctionRepository>();
        services.AddScoped<MasterRepository>();
        services.AddScoped<OrderSparePartRepository>();
        services.AddScoped<OrderMalfunctionRepository>();
        services.AddScoped<OrderWorkRepository>();

        // Добавьте другие сервисы по мере необходимости.
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage(); // Отображение страницы ошибок в режиме разработки.
        }
        else
        {
            app.UseExceptionHandler("/Home/Error"); // Обработка ошибок в продакшене.
            app.UseHsts(); // Использование HSTS.
        }

        app.UseHttpsRedirection(); // Перенаправление HTTP на HTTPS.
        app.UseStaticFiles(); // Разрешение статических файлов.

        app.UseRouting(); // Настройка маршрутизации.

        app.UseAuthorization(); // Разрешение авторизации.

        app.UseEndpoints(endpoints =>
        {
            // Главная страница (редирект на Order/Index)
            endpoints.MapGet("/", async context =>
            {
                context.Response.Redirect("/Order/Index");
            });

            // Основные маршруты для контроллера Order
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "Order/{action=Index}/{id?}",
                defaults: new { controller = "Order" }
            );
        });
    }
}