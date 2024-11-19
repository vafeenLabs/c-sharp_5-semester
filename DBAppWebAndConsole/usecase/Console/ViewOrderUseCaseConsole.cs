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
                Console.WriteLine(
                    $" - {om.Malfunction.Description} (ID: {om.Malfunction.IdMalfunction})"
                );
            }
        }
        else
        {
            Console.WriteLine("Заказ с таким ID не найден.");
        }
    }
}
