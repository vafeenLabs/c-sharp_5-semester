public class EditOrderUseCaseWeb
{
    public async Task<OrderViewModel> Get(int id)
    {
        var orderRepo = new OrderRepository();
        var sparePartRepo = new SparePartRepository();
        var workRepo = new WorkRepository();
        var malfunctionRepo = new MalfunctionRepository();
        var masterRepo = new MasterRepository();

        var order = await orderRepo.GetByIdAsync(id);
        if (order == null) return null;

        var model = new OrderViewModel
        {
            IdOrder = order.IdOrder,
            IdMaster = order.IdMaster,
            SelectedSpareParts = order.SpareParts
                .ToList(),
            SelectedWorks = order.Works
                .ToList(),
            SelectedMalfunctions = order.Malfunctions
                .ToList(),
            SpareParts = await sparePartRepo.GetAllAsync(),
            Works = await workRepo.GetAllAsync(),
            Malfunctions = await malfunctionRepo.GetAllAsync(),
            Masters = await masterRepo.GetAllAsync()
        };

        return model;
    }

    public async Task<bool> Post(int id, OrderViewModel model)
    {
        var orderRepo = new OrderRepository();
        var order = await orderRepo.GetByIdAsync(id);

        order.IdMaster = model.IdMaster;
        await orderRepo.UpdateAsync(order);
        await orderRepo.UpdateOrderDetails(order.IdOrder, model.SelectedSpareParts, model.SelectedWorks, model.SelectedMalfunctions);

        return true;
    }
}
