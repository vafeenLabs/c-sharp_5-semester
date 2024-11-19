public class DeleteOrderUseCaseConsole
{
    public static async Task DeleteOrder()
    {
        var orderRepo = new OrderRepository();

        await ViewOrderUseCaseConsole.ViewOrders();

        Console.WriteLine("Введите ID заказа для удаления:");
        var orderId = Convert.ToInt32(Console.ReadLine());

        var orderToDelete = await orderRepo.GetByIdAsync(orderId);

        if (orderToDelete != null)
        {
            await orderRepo.DeleteOrderDetails(orderToDelete.IdOrder);

            await orderRepo.DeleteAsync(orderToDelete);

            Console.WriteLine($"Заказ с ID {orderId} был успешно удалён.");
        }
        else
        {
            Console.WriteLine("Заказ с таким ID не найден.");
        }
    }
}
