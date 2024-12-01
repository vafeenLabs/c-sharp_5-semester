public class CreateOrderUseCaseConsole
{
    public static async Task CreateOrder()
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
        if (
            !int.TryParse(masterIdInput, out var masterId)
            || !masters.Any(m => m.IdMaster == masterId)
        )
        {
            Console.WriteLine("Некорректный ID мастера.");
            return;
        }

        // Создание нового заказа
        var newOrder = new Order
        {
            Comment = comment,
            IdMaster =
                masterId // Используем ID выбранного мастера
            ,
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
                    await orderSparePartRepo.AddAsync(
                        new OrderSparePart { IdOrder = newOrder.IdOrder, IdSparePart = sparePartId }
                    );
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
                    await orderWorkRepo.AddAsync(
                        new OrderWork { IdOrder = newOrder.IdOrder, IdWork = work.IdWork }
                    );
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
            Console.WriteLine(
                $"ID: {malfunction.IdMalfunction}, Описание: {malfunction.Description}"
            );
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
                    await orderMalfunctionRepo.AddAsync(
                        new OrderMalfunction
                        {
                            IdOrder = newOrder.IdOrder,
                            IdMalfunction = malfunction.IdMalfunction,
                        }
                    );
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
}
