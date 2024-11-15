using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    static async Task Main(string[] args)
    {
        // Инициализация базы данных
        // await InitializeDatabase();

        // Основная часть программы
        await MainPart();
    }

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
                    await CreateOrder();
                    break;
                case "2":
                    await ViewOrders();
                    break;
                case "3":
                    await UpdateOrder();
                    break;
                case "4":
                    await DeleteOrder();
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

    // 1. Сделать заказ



    private static async Task CreateOrder()
    {
        var orderRepo = new OrderRepository();
        var sparePartRepo = new SparePartRepository();
        var workRepo = new WorkRepository();
        var malfunctionRepo = new MalfunctionRepository();
        var masterRepo = new MasterRepository();
        var personRepo = new PersonRepository();


        var orderSparePartRepo = new OrderSparePartRepository();
        var orderWorkRepo = new OrderWorkRepository();
        var orderMalfunctionRepo = new OrderMalfunctionRepository();

        Console.WriteLine("Создание нового заказа.");

        // Ввод данных для нового заказа
        Console.WriteLine("Введите комментарий:");
        var comment = Console.ReadLine();

        // Вывод списка мастеров
        var masters = await masterRepo.GetAllAsync();
        if (masters == null || !masters.Any())
        {
            Console.WriteLine("Нет доступных мастеров.");
            return;
        }

        Console.WriteLine("Выберите мастера (ID):");
        foreach (var master in masters)
        {
            // Проверка на наличие объекта Person
            var person = await personRepo.GetAsync(master.PersonId);
            Console.WriteLine($"{master.IdMaster}. {person.Name} ({master.Specialization})");
        }

        Console.WriteLine("Введите ID мастера:");
        var masterIdInput = Console.ReadLine();

        // Проверка корректности ввода ID мастера
        if (!int.TryParse(masterIdInput, out var masterId) || !masters.Any(m => m.IdMaster == masterId))
        {
            Console.WriteLine("Некорректный ID мастера.");
            return;
        }

        // Создание нового заказа
        var newOrder = new Order
        {
            Comment = comment,
            IdMaster = masterId // Используем ID выбранного мастера
                                // Можно добавить другие необходимые поля, если они есть
        };

        // Добавление нового заказа в репозиторий
        await orderRepo.AddAsync(newOrder);

        // Вывод доступных запасных частей
        Console.WriteLine("Доступные запасные части:");
        var spareParts = await sparePartRepo.GetAllAsync();

        foreach (var sparePart in spareParts)
        {
            Console.WriteLine($"ID: {sparePart.IdSparePart}, Название: {sparePart.Name}");
        }

        // Обновление запасных частей
        Console.WriteLine("Введите ID запасных частей через пробел:");
        var sparePartsInput = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(sparePartsInput))
        {
            var sparePartsIds = sparePartsInput.Split(' ').Select(int.Parse).ToList();

            foreach (var sparePartId in sparePartsIds)
            {
                // Получаем запасную часть по ID
                var sparePart = await sparePartRepo.GetAsync(sparePartId);
                if (sparePart != null)
                {
                    await orderSparePartRepo.AddAsync(new OrderSparePart { IdOrder = newOrder.IdOrder, IdSparePart = sparePartId });
                    Console.WriteLine($"Запасная часть с ID {sparePartId} добавлена к заказу.");
                }
                else
                {
                    Console.WriteLine($"Запасная часть с ID {sparePartId} не найдена.");
                }
            }
        }

        // Вывод доступных работ
        Console.WriteLine("Доступные работы:");
        var works = await workRepo.GetAllAsync();

        foreach (var work in works)
        {
            Console.WriteLine($"ID: {work.IdWork}, Описание: {work.WorkDescription}");
        }

        // Обновление работ
        Console.WriteLine("Введите ID работ через пробел:");
        var worksInput = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(worksInput))
        {
            var worksIds = worksInput.Split(' ').Select(int.Parse).ToList();

            foreach (var workId in worksIds)
            {
                // Получаем работу по ID
                var work = await workRepo.GetAsync(workId);
                if (work != null)
                {
                    await orderWorkRepo.AddAsync(new OrderWork { IdOrder = newOrder.IdOrder, IdWork = work.IdWork });
                    Console.WriteLine($"Работа с ID {workId} добавлена к заказу.");
                }
                else
                {
                    Console.WriteLine($"Работа с ID {workId} не найдена.");
                }
            }
        }

        // Вывод доступных неисправностей
        Console.WriteLine("Доступные неисправности:");
        var malfunctions = await malfunctionRepo.GetAllAsync();

        foreach (var malfunction in malfunctions)
        {
            Console.WriteLine($"ID: {malfunction.IdMalfunction}, Описание: {malfunction.Description}");
        }

        // Обновление неисправностей
        Console.WriteLine("Введите ID неисправностей через пробел:");
        var malfunctionsInput = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(malfunctionsInput))
        {
            var malfunctionsIds = malfunctionsInput.Split(' ').Select(int.Parse).ToList();

            foreach (var malfunctionId in malfunctionsIds)
            {
                // Получаем неисправность по ID
                var malfunction = await malfunctionRepo.GetAsync(malfunctionId);
                if (malfunction != null)
                {
                    await orderMalfunctionRepo.AddAsync(new OrderMalfunction { IdOrder = newOrder.IdOrder, IdMalfunction = malfunction.IdMalfunction });
                    Console.WriteLine($"Неисправность с ID {malfunctionId} добавлена к заказу.");
                }
                else
                {
                    Console.WriteLine($"Неисправность с ID {malfunctionId} не найдена.");
                }
            }
        }

        Console.WriteLine($"Заказ с ID {newOrder.IdOrder} успешно создан.");
    }



    // 2. Посмотреть заказы
    private static async Task ViewOrders()
    {
        var orderRepo = new OrderRepository();
        var orders = await orderRepo.GetAllAsync();

        if (orders.Any())
        {
            Console.WriteLine("Список заказов:");
            foreach (var ord in orders)
            {
                Console.WriteLine($"Order ID: {ord.IdOrder}");
                Console.WriteLine($"Comment: {ord.Comment}");
                Console.WriteLine();
            }

            // Запрос на подробный просмотр
            Console.WriteLine("Введите ID заказа для подробного просмотра или 'exit' для выхода:");
            string input = Console.ReadLine();

            if (input.ToLower() != "exit" && int.TryParse(input, out int orderId))
            {
                await ViewOrderDetails(orderId);
            }
        }
        else
        {
            Console.WriteLine("Нет заказов.");
        }
    }


    private static async Task ViewOrderDetails(int orderId)
    {
        var orderRepo = new OrderRepository();
        var orderToView = await orderRepo.GetByIdAsync(orderId);

        if (orderToView != null)
        {
            Console.WriteLine($"Подробности заказа ID: {orderToView.IdOrder}");
            Console.WriteLine($"Комментарий: {orderToView.Comment}");

            Console.WriteLine("Запасные части:");
            foreach (var osp in orderToView.OrderSpareParts)
            {
                Console.WriteLine($" - {osp.SparePart.Name} (ID: {osp.SparePart.IdSparePart})");
            }

            Console.WriteLine("Работы:");
            foreach (var ow in orderToView.OrderWorks)
            {
                Console.WriteLine($" - {ow.Work.WorkDescription} (ID: {ow.Work.IdWork})");
            }

            Console.WriteLine("Неисправности:");
            foreach (var om in orderToView.OrderMalfunctions)
            {
                Console.WriteLine($" - {om.Malfunction.Description} (ID: {om.Malfunction.IdMalfunction})");
            }
        }
        else
        {
            Console.WriteLine("Заказ с таким ID не найден.");
        }
    }

    // 3. Обновить заказ
    private static async Task UpdateOrder()
    {
        var orderRepo = new OrderRepository();
        var sparePartRepo = new SparePartRepository();
        var workRepo = new WorkRepository();
        var malfunctionRepo = new MalfunctionRepository();
        var masterRepo = new MasterRepository();
        var personRepo = new PersonRepository();

        // Для начала смотрим заказы
        await ViewOrders();

        Console.WriteLine("Введите ID заказа для обновления:");
        var orderId = Convert.ToInt32(Console.ReadLine());

        var orderToUpdate = await orderRepo.GetByIdAsync(orderId);

        if (orderToUpdate != null)
        {
            Console.WriteLine("Введите новый комментарий для заказа:");
            orderToUpdate.Comment = Console.ReadLine();

            // Вывод списка мастеров
            var masters = await masterRepo.GetAllAsync();
            Console.WriteLine("Выберите мастера (ID):");
            foreach (var master in masters)
            {
                Console.WriteLine($"{master.IdMaster}. {master.Person.Name} ({master.Specialization})");
            }
            Console.WriteLine("Введите новый ID мастера:");
            orderToUpdate.IdMaster = Convert.ToInt32(Console.ReadLine());

            // Обновление запасных частей
            Console.WriteLine("Текущие запасные части в заказе:");
            foreach (var osp in orderToUpdate.OrderSpareParts)
            {
                Console.WriteLine($" - {osp.SparePart.Name} (ID: {osp.SparePart.IdSparePart})");
            }
            Console.WriteLine("Введите новые ID запасных частей через пробел:");
            var newSpareParts = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

            // Обновление работ
            Console.WriteLine("Текущие работы в заказе:");
            foreach (var ow in orderToUpdate.OrderWorks)
            {
                Console.WriteLine($" - {ow.Work.WorkDescription} (ID: {ow.Work.IdWork})");
            }
            Console.WriteLine("Введите новые ID работ через пробел:");
            var newWorks = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

            // Обновление неисправностей
            Console.WriteLine("Текущие неисправности в заказе:");
            foreach (var om in orderToUpdate.OrderMalfunctions)
            {
                Console.WriteLine($" - {om.Malfunction.Description} (ID: {om.Malfunction.IdMalfunction})");
            }
            Console.WriteLine("Введите новые ID неисправностей через пробел:");
            var newMalfunctions = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

            // Обновляем детали заказа
            await orderRepo.UpdateOrderDetails(orderToUpdate.IdOrder, newSpareParts, newWorks, newMalfunctions);

            // Обновляем сам заказ
            await orderRepo.UpdateAsync(orderToUpdate);

            Console.WriteLine($"Заказ с ID {orderToUpdate.IdOrder} был успешно обновлён.");
        }
        else
        {
            Console.WriteLine("Заказ с таким ID не найден.");
        }
    }

    // 4. Удалить заказ
    private static async Task DeleteOrder()
    {
        var orderRepo = new OrderRepository();
        // Для начала смотрим заказы
        await ViewOrders();

        Console.WriteLine("Введите ID заказа для удаления:");
        var orderId = Convert.ToInt32(Console.ReadLine());

        var orderToDelete = await orderRepo.GetByIdAsync(orderId);

        if (orderToDelete != null)
        {
            // Удаляем детали заказа
            await orderRepo.DeleteOrderDetails(orderToDelete.IdOrder);

            // Удаляем сам заказ
            await orderRepo.DeleteAsync(orderToDelete);

            Console.WriteLine($"Заказ с ID {orderId} был успешно удалён.");
        }
        else
        {
            Console.WriteLine("Заказ с таким ID не найден.");
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
        await masterRepo.AddAsync(new Master { Date = DateTime.Now, Specialization = "Поломка двигателя", Experience = 4, WorkRate = 90, PersonId = 2 }); // Убедитесь, что PersonId соответствует существующему пользователю
        await masterRepo.AddAsync(new Master { Date = DateTime.Now, Specialization = "Накачать шины", Experience = 3, WorkRate = 80, PersonId = 3 });
        await masterRepo.AddAsync(new Master { Date = DateTime.Now, Specialization = "Сдать металлолом", Experience = 6, WorkRate = 110, PersonId = 4 });
        await masterRepo.AddAsync(new Master { Date = DateTime.Now, Specialization = "Затонировать окна", Experience = 7, WorkRate = 120, PersonId = 5 });

        // Добавление запасных частей (SpareParts)
        await sparePartRepo.AddAsync(new SparePart { Name = "Зимние колеса", Price = 150 });
        await sparePartRepo.AddAsync(new SparePart { Name = "Всесезонные колеса", Price = 130 });
        await sparePartRepo.AddAsync(new SparePart { Name = "Летние колеса", Price = 120 });

        // Добавление неисправностей (Malfunctions)
        await malfunctionRepo.AddAsync(new Malfunction { IdMalfunction = 1, Description = "Полетел движок" });
        await malfunctionRepo.AddAsync(new Malfunction { IdMalfunction = 2, Description = "Колесо сдуто" });
        await malfunctionRepo.AddAsync(new Malfunction { IdMalfunction = 3, Description = "Лампочка не горит" });

        // Добавление работ (Works)
        await workRepo.AddAsync(new Work { IdWork = 1, WorkDescription = "Покрасить кузов" });
        await workRepo.AddAsync(new Work { IdWork = 2, WorkDescription = "Заменить колесо" });
        await workRepo.AddAsync(new Work { IdWork = 3, WorkDescription = "Заменить моторное масло" });
    }
}
