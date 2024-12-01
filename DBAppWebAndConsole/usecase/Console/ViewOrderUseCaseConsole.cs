public class ViewOrderUseCaseConsole
{
    public static async Task ViewOrders()
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

    public static async Task ViewOrderDetails(int orderId)
    {
        var orderRepo = new OrderRepository();
        var orderToView = await orderRepo.GetByIdAsync(orderId);

        if (orderToView != null)
        {
            Console.WriteLine($"Подробности заказа ID: {orderToView.IdOrder}");
            Console.WriteLine($"Комментарий: {orderToView.Comment}");

            Console.WriteLine("Запасные части:");
            foreach (var sparePart in orderToView.SpareParts)
            {
                Console.WriteLine($" - {sparePart.Name} (ID: {sparePart.IdSparePart})");
            }

            Console.WriteLine("Работы:");
            foreach (var work in orderToView.Works)
            {
                Console.WriteLine($" - {work.WorkDescription} (ID: {work.IdWork})");
            }

            Console.WriteLine("Неисправности:");
            foreach (var malfunction in orderToView.Malfunctions)
            {
                Console.WriteLine(
                    $" - {malfunction.Description} (ID: {malfunction.IdMalfunction})"
                );
            }
        }
        else
        {
            Console.WriteLine("Заказ с таким ID не найден.");
        }
    }
}
