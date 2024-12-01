using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Настройка MVC
        services.AddControllersWithViews();

        // Регистрация репозиториев
        services.AddScoped<OrderRepository>();
        services.AddScoped<SparePartRepository>();
        services.AddScoped<WorkRepository>();
        services.AddScoped<MalfunctionRepository>();
        services.AddScoped<MasterRepository>();
        services.AddScoped<CreateOrderUseCaseWeb>();
        services.AddScoped<EditOrderUseCaseWeb>();
        services.AddScoped<DetailsUseCaseWeb>();
        services.AddScoped<DeleteOrderUseCaseWeb>();

        // Настройка сессий
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Устанавливаем время жизни сессии
            options.Cookie.HttpOnly = true; // Защищаем cookie от доступа через JS
            options.Cookie.SameSite = SameSiteMode.Strict; // Ограничиваем использование cookie только на том же сайте
        });

        // Настройка cookie политики
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // Включаем работу с https
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        // Подключаем сессии
        app.UseSession();

        // Конфигурация маршрутов
        app.UseRouting();

        // Включаем аутентификацию
        app.UseAuthentication();

        // Включаем авторизацию
        app.UseAuthorization();

        // Конфигурация конечных точек маршрутов
        app.UseEndpoints(endpoints =>
        {
            // Переадресовываем на страницу заказов по умолчанию
            endpoints.MapGet("/", context =>
            {
                context.Response.Redirect("/Order/Index");
                return Task.CompletedTask;
            });

            // Маршруты для контроллеров
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "Order/{action=Index}/{id?}",
                defaults: new { controller = "Order" }
            );

            // Маршруты для страницы профиля и аутентификации
            endpoints.MapControllerRoute(
                name: "account",
                pattern: "Account/{action=Index}/{id?}",
                defaults: new { controller = "Account" }
            );
        });
    }
}
