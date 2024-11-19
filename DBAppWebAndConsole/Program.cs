using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // for console version
        // await InitializeDatabase();
        // await MainPart();

        // for web version
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

    // Основная часть программы
    private static async Task MainPart()
    {
        // Меню
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Сделать заказ");
            Console.WriteLine("2. Посмотреть заказы");
            Console.WriteLine("3. Обновить заказ");
            Console.WriteLine("4. Удалить заказ");
            Console.WriteLine("5. Выход");

            Console.Write("Выберите действие: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateOrderUseCaseConsole.CreateOrder();
                    break;
                case "2":
                    await ViewOrderUseCaseConsole.ViewOrders();
                    break;
                case "3":
                    await UpdateOrderUseCaseConsole.UpdateOrder();
                    break;
                case "4":
                    await DeleteOrderUseCaseConsole.DeleteOrder();
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, выберите снова.");
                    break;
            }
        }
    }

    // Инициализация базы данных
    private static async Task InitializeDatabase()
    {
        var sparePartRepo = new SparePartRepository();
        var workRepo = new WorkRepository();
        var malfunctionRepo = new MalfunctionRepository();
        var masterRepo = new MasterRepository();
        var personRepo = new PersonRepository();

        // Создаем пользователей (Persons)
        var person1 = new Person { IdPerson = 1, Name = "Артур" };
        var person2 = new Person { IdPerson = 2, Name = "Ярик" };
        var person3 = new Person { IdPerson = 3, Name = "Данил" };
        var person4 = new Person { IdPerson = 4, Name = "Миша" };
        var person5 = new Person { IdPerson = 5, Name = "Андрей" };

        await personRepo.AddAsync(person1);
        await personRepo.AddAsync(person2);
        await personRepo.AddAsync(person3);
        await personRepo.AddAsync(person4);
        await personRepo.AddAsync(person5);

        // Создание мастеров (Masters)
        await masterRepo.AddAsync(
            new Master
            {
                Date = DateTime.Now,
                Specialization = "Поломка двигателя",
                Experience = 4,
                WorkRate = 90,
                PersonId = 2,
            }
        ); // Убедитесь, что PersonId соответствует существующему пользователю
        await masterRepo.AddAsync(
            new Master
            {
                Date = DateTime.Now,
                Specialization = "Накачать шины",
                Experience = 3,
                WorkRate = 80,
                PersonId = 3,
            }
        );
        await masterRepo.AddAsync(
            new Master
            {
                Date = DateTime.Now,
                Specialization = "Сдать металлолом",
                Experience = 6,
                WorkRate = 110,
                PersonId = 4,
            }
        );
        await masterRepo.AddAsync(
            new Master
            {
                Date = DateTime.Now,
                Specialization = "Затонировать окна",
                Experience = 7,
                WorkRate = 120,
                PersonId = 5,
            }
        );

        // Добавление запасных частей (SpareParts)
        await sparePartRepo.AddAsync(new SparePart { Name = "Зимние колеса", Price = 150 });
        await sparePartRepo.AddAsync(new SparePart { Name = "Всесезонные колеса", Price = 130 });
        await sparePartRepo.AddAsync(new SparePart { Name = "Летние колеса", Price = 120 });

        // Добавление неисправностей (Malfunctions)
        await malfunctionRepo.AddAsync(
            new Malfunction { IdMalfunction = 1, Description = "Полетел движок" }
        );
        await malfunctionRepo.AddAsync(
            new Malfunction { IdMalfunction = 2, Description = "Колесо сдуто" }
        );
        await malfunctionRepo.AddAsync(
            new Malfunction { IdMalfunction = 3, Description = "Лампочка не горит" }
        );

        // Добавление работ (Works)
        await workRepo.AddAsync(new Work { IdWork = 1, WorkDescription = "Покрасить кузов" });
        await workRepo.AddAsync(new Work { IdWork = 2, WorkDescription = "Заменить колесо" });
        await workRepo.AddAsync(
            new Work { IdWork = 3, WorkDescription = "Заменить моторное масло" }
        );
    }
}
