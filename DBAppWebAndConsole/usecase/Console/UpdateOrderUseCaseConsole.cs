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
            foreach (var osp in orderToUpdate.OrderSpareParts)
            {
                Console.WriteLine($" - {osp.SparePart.Name} (ID: {osp.SparePart.IdSparePart})");
            }

            // Вывод списка всех доступных запасных частей
            var spareParts = await sparePartRepo.GetAllAsync();
            Console.WriteLine("Доступные запасные части:");
            foreach (var sparePart in spareParts)
            {
                Console.WriteLine($"ID: {sparePart.IdSparePart}, Название: {sparePart.Name}");
            }

            Console.WriteLine("Введите новые ID запасных частей через пробел:");
            var newSpareParts = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

            // Обновление работ
            Console.WriteLine("Текущие работы в заказе:");
            foreach (var ow in orderToUpdate.OrderWorks)
            {
                Console.WriteLine($" - {ow.Work.WorkDescription} (ID: {ow.Work.IdWork})");
            }

            // Вывод списка всех доступных работ
            var works = await workRepo.GetAllAsync();
            Console.WriteLine("Доступные работы:");
            foreach (var work in works)
            {
                Console.WriteLine($"ID: {work.IdWork}, Описание: {work.WorkDescription}");
            }

            Console.WriteLine("Введите новые ID работ через пробел:");
            var newWorks = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

            // Обновление неисправностей
            Console.WriteLine("Текущие неисправности в заказе:");
            foreach (var om in orderToUpdate.OrderMalfunctions)
            {
                Console.WriteLine(
                    $" - {om.Malfunction.Description} (ID: {om.Malfunction.IdMalfunction})"
                );
            }

            // Вывод списка всех доступных неисправностей
            var malfunctions = await malfunctionRepo.GetAllAsync();
            Console.WriteLine("Доступные неисправности:");
            foreach (var malfunction in malfunctions)
            {
                Console.WriteLine(
                    $"ID: {malfunction.IdMalfunction}, Описание: {malfunction.Description}"
                );
            }

            Console.WriteLine("Введите новые ID неисправностей через пробел:");
            var newMalfunctions = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

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
