public class DetailsUseCaseWeb
{
    public async Task<OrderViewModel> Get(int id)
    {
        var orderRepo = new OrderRepository();
        var sparePartRepo = new SparePartRepository();
        var workRepo = new WorkRepository();
        var malfunctionRepo = new MalfunctionRepository();
        var masterRepo = new MasterRepository();
        var order = await orderRepo.GetByIdAsync(id);

        var model = new OrderViewModel
        {
            IdOrder = order.IdOrder,
            IdMaster = order.IdMaster,

            SelectedSpareParts = order.SpareParts.ToList(),

            SelectedWorks = order.Works.ToList(),
            SelectedMalfunctions = order.Malfunctions.ToList(),

            SpareParts = await sparePartRepo.GetAllAsync(),
            Works = await workRepo.GetAllAsync(),
            Malfunctions = await malfunctionRepo.GetAllAsync(),
            Masters = await masterRepo.GetAllAsync(),
        };

        return model;
    }
}
