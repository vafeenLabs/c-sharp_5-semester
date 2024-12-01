public class CreateOrderUseCaseWeb
{
    public async Task<OrderViewModel> Get()
    {
        var sparePartRepo = new SparePartRepository();
        var workRepo = new WorkRepository();
        var malfunctionRepo = new MalfunctionRepository();
        var masterRepo = new MasterRepository();

        var model = new OrderViewModel
        {
            SpareParts = await sparePartRepo.GetAllAsync(),
            Works = await workRepo.GetAllAsync(),
            Malfunctions = await malfunctionRepo.GetAllAsync(),
            Masters = await masterRepo.GetAllAsync(),
        };

        return model;
    }

    public async Task<bool> Post(OrderViewModel model)
    {
        var orderRepo = new OrderRepository();

        var order = new Order
        {
            IdMaster = model.IdMaster,
        };

        await orderRepo.AddAsync(order);
        await orderRepo.UpdateOrderDetails(
            order.IdOrder,
            model.SelectedSpareParts.ToList(),
            model.SelectedWorks.ToList(),
            model.SelectedMalfunctions.ToList()
        );

        return true;
    }
}
