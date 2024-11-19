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
            endpoints.MapControllerRoute(
                name: "home",
                pattern: "/",
                defaults: new { controller = "Order", action = "Index" }
            );

            // Создание заказа
            endpoints.MapControllerRoute(
                name: "createOrder",
                pattern: "Order/Create",
                defaults: new { controller = "Order", action = "Create" }
            );

            // Просмотр заказа
            endpoints.MapControllerRoute(
                name: "viewOrder",
                pattern: "Order/Details/{id}",
                defaults: new { controller = "Order", action = "Details" }
            );

            // Обновление заказа
            endpoints.MapControllerRoute(
                name: "editOrder",
                pattern: "Order/Edit/{id}",
                defaults: new { controller = "Order", action = "Edit" }
            );

            // Удаление заказа
            endpoints.MapControllerRoute(
                name: "deleteOrder",
                pattern: "Order/Delete/{id}",
                defaults: new { controller = "Order", action = "Delete" }
            );
        });
    }
}
