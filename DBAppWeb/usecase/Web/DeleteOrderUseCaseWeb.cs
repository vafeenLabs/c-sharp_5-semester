public class DeleteOrderUseCaseWeb
{
    public async Task<Order> Get(int id)
    {
        var orderRepo = new OrderRepository();
        return await orderRepo.GetByIdAsync(id);
    }

    public async Task<bool> Post(int id)
    {
        var orderRepo = new OrderRepository();
        var order = await orderRepo.GetByIdAsync(id);

        if (order == null)
            return false;

        await orderRepo.DeleteOrderDetails(order.IdOrder);
        await orderRepo.DeleteAsync(order);
        return true;
    }
}
