public class UpdateOrderUseCaseConsole
{
    public static async Task UpdateOrder()
    {
        var orderRepo = new OrderRepository();
        var sparePartRepo = new SparePartRepository();
        var workRepo = new WorkRepository();
        var malfunctionRepo = new MalfunctionRepository();
        var masterRepo = new MasterRepository();
        var personRepo = new PersonRepository();

        // Для начала смотрим заказы
        await ViewOrderUseCaseConsole.ViewOrders();

        Console.WriteLine("Введите ID заказа для обновления:");
        var orderId = Convert.ToInt32(Console.ReadLine());

        var orderToUpdate = await orderRepo.GetByIdAsync(orderId);

        if (orderToUpdate != null)
        {
            Console.WriteLine("Введите новый комментарий для заказа:");
            orderToUpdate.Comment = Console.ReadLine();

            // Выводим текущего мастера
            var currentMaster = await masterRepo.GetAsync(orderToUpdate.IdMaster);
            if (currentMaster != null)
            {
                var currentPerson = await personRepo.GetAsync(currentMaster.PersonId);
                Console.WriteLine(
                    $"Текущий мастер: {currentMaster.IdMaster}. {currentPerson.Name} ({currentMaster.Specialization})"
                );
            }
            else
            {
                Console.WriteLine("Текущий мастер не найден.");
            }

            // Вывод списка мастеров
            var masters = await masterRepo.GetAllAsync();
            Console.WriteLine("Выберите нового мастера (ID):");
            foreach (var master in masters)
            {
                var person = await personRepo.GetAsync(master.PersonId);
                Console.WriteLine($"{master.IdMaster}. {person.Name} ({master.Specialization})");
            }

            Console.WriteLine("Введите новый ID мастера:");
            orderToUpdate.IdMaster = Convert.ToInt32(Console.ReadLine());

            // Обновление запасных частей
            Console.WriteLine("Текущие запасные части в заказе:");
            foreach (var sparePart in orderToUpdate.SpareParts)
            {
                Console.WriteLine($" - {sparePart.Name} (ID: {sparePart.IdSparePart})");
            }

            // Вывод списка всех доступных запасных частей
            var spareParts = await sparePartRepo.GetAllAsync();
            Console.WriteLine("Доступные запасные части:");
            foreach (var sparePart in spareParts)
            {
                Console.WriteLine($"ID: {sparePart.IdSparePart}, Название: {sparePart.Name}");
            }

            Console.WriteLine("Введите новые ID запасных частей через пробел:");
            var newSparePartsId = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            var newSpareParts = new List<SparePart>();
            foreach (var sparePartId in newSparePartsId)
            {
                newSpareParts.Add(await sparePartRepo.GetAsync(sparePartId));
            }

            // Обновление работ
            Console.WriteLine("Текущие работы в заказе:");
            foreach (var work in orderToUpdate.Works)
            {
                Console.WriteLine($" - {work.WorkDescription} (ID: {work.IdWork})");
            }

            // Вывод списка всех доступных работ
            var works = await workRepo.GetAllAsync();
            Console.WriteLine("Доступные работы:");
            foreach (var work in works)
            {
                Console.WriteLine($"ID: {work.IdWork}, Описание: {work.WorkDescription}");
            }

            Console.WriteLine("Введите новые ID работ через пробел:");
            var newWorksId = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            var newWorks = new List<Work>();
            foreach (var workId in newWorksId)
            {
                newWorks.Add(await workRepo.GetAsync(workId));
            }

            // Обновление неисправностей
            Console.WriteLine("Текущие неисправности в заказе:");
            foreach (var om in orderToUpdate.Malfunctions)
            {
                Console.WriteLine($" - {om.Description} (ID: {om.IdMalfunction})");
            }

            // Вывод списка всех доступных неисправностей
            var malfunctions = await malfunctionRepo.GetAllAsync();
            Console.WriteLine("Доступные неисправности:");
            foreach (var malfunction in malfunctions)
            {
                Console.WriteLine($"ID: {malfunction.IdMalfunction}, Описание: {malfunction.Description}");
            }

            Console.WriteLine("Введите новые ID неисправностей через пробел:");
            var newMalfunctionsId = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            var newMalfunctions = new List<Malfunction>();
            foreach (var malfunctionId in newMalfunctionsId)
            {
                newMalfunctions.Add(await malfunctionRepo.GetAsync(malfunctionId));
            }

            // Обновляем детали заказа
            await orderRepo.UpdateOrderDetails(
                orderToUpdate.IdOrder,
                newSpareParts,
                newWorks,
                newMalfunctions
            );

            // Обновляем сам заказ
            await orderRepo.UpdateAsync(orderToUpdate);

            Console.WriteLine($"Заказ с ID {orderToUpdate.IdOrder} был успешно обновлён.");
        }
        else
        {
            Console.WriteLine("Заказ с таким ID не найден.");
        }
    }
}
