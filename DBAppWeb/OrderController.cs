using Microsoft.AspNetCore.Mvc;

public class OrderController : Controller
{
    private readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger)
    {
        _logger = logger;
    }

    // GET: Order/Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var sparePartRepo = new SparePartRepository();
        var workRepo = new WorkRepository();
        var malfunctionRepo = new MalfunctionRepository();
        var masterRepo = new MasterRepository();

        ViewBag.SpareParts = await sparePartRepo.GetAllAsync();
        ViewBag.Works = await workRepo.GetAllAsync();
        ViewBag.Malfunctions = await malfunctionRepo.GetAllAsync();
        ViewBag.Masters = await masterRepo.GetAllAsync();

        return View(new OrderViewModel());
    }

    // GET: Order/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var orderRepo = new OrderRepository();
        var sparePartRepo = new SparePartRepository();
        var workRepo = new WorkRepository();
        var malfunctionRepo = new MalfunctionRepository();
        var masterRepo = new MasterRepository();
        var orderSparePartRepo = new OrderSparePartRepository();
        var orderMalfunctionRepo = new OrderMalfunctionRepository();
        var orderWorkRepo = new OrderWorkRepository();

        var order = await orderRepo.GetByIdAsync(id);
        if (order == null)
        {
            return RedirectToAction("Index");
        }

        var model = new OrderViewModel
        {
            IdOrder = order.IdOrder,
            IdMaster = order.IdMaster,

            // Получаем все данные, затем фильтруем по IdOrder
            SelectedSpareParts = (await orderSparePartRepo.GetAllAsync())
                .Where(osp => osp.IdOrder == order.IdOrder)
                .Select(osp => osp.IdSparePart)
                .ToList(),

            SelectedWorks = (await orderWorkRepo.GetAllAsync())
                .Where(ow => ow.IdOrder == order.IdOrder)
                .Select(ow => ow.IdWork)
                .ToList(),

            SelectedMalfunctions = (await orderMalfunctionRepo.GetAllAsync())
                .Where(om => om.IdOrder == order.IdOrder)
                .Select(om => om.IdMalfunction)
                .ToList(),

            // Заполнение других свойств
        };

        ViewBag.SpareParts = await sparePartRepo.GetAllAsync();
        ViewBag.Works = await workRepo.GetAllAsync();
        ViewBag.Malfunctions = await malfunctionRepo.GetAllAsync();
        ViewBag.Masters = await masterRepo.GetAllAsync();

        return View(model);
    }



    // POST: Order/Create
    [HttpPost]
    public async Task<IActionResult> Create(OrderViewModel model)
    {
        if (ModelState.IsValid)
        {
            _logger.LogInformation($"Received Model: IdMaster: {model.IdMaster}");

            var orderRepo = new OrderRepository();

            var order = new Order
            {
                IdMaster = model.IdMaster
                // Заполнение других свойств заказа
            };
            await orderRepo.AddAsync(order);

            await orderRepo.UpdateOrderDetails(order.IdOrder, model.SelectedSpareParts, model.SelectedWorks, model.SelectedMalfunctions);

            return RedirectToAction("Index");
        }

        var sparePartRepo = new SparePartRepository();
        var workRepo = new WorkRepository();
        var malfunctionRepo = new MalfunctionRepository();
        var masterRepo = new MasterRepository();

        ViewBag.SpareParts = await sparePartRepo.GetAllAsync();
        ViewBag.Works = await workRepo.GetAllAsync();
        ViewBag.Malfunctions = await malfunctionRepo.GetAllAsync();
        ViewBag.Masters = await masterRepo.GetAllAsync();

        return View(model);
    }

    // GET: Order/Index
    public async Task<IActionResult> Index()
    {
        var orderRepo = new OrderRepository();
        var orders = await orderRepo.GetAllAsync();
        return View(orders);
    }


    // GET: Order/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var orderRepo = new OrderRepository();
        var sparePartRepo = new SparePartRepository();
        var workRepo = new WorkRepository();
        var malfunctionRepo = new MalfunctionRepository();
        var masterRepo = new MasterRepository();
        var orderSparePartRepo = new OrderSparePartRepository();
        var orderMalfunctionRepo = new OrderMalfunctionRepository();
        var orderWorkRepo = new OrderWorkRepository();


        var order = await orderRepo.GetByIdAsync(id);
        if (order == null)
        {
            return RedirectToAction("Index");
        }

        var model = new OrderViewModel
        {
            IdOrder = order.IdOrder,
            IdMaster = order.IdMaster,

            // Получаем все данные, затем фильтруем по IdOrder
            SelectedSpareParts = (await orderSparePartRepo.GetAllAsync())
                .Where(osp => osp.IdOrder == order.IdOrder)
                .Select(osp => osp.IdSparePart)
                .ToList(),

            SelectedWorks = (await orderWorkRepo.GetAllAsync())
                .Where(ow => ow.IdOrder == order.IdOrder)
                .Select(ow => ow.IdWork)
                .ToList(),

            SelectedMalfunctions = (await orderMalfunctionRepo.GetAllAsync())
                .Where(om => om.IdOrder == order.IdOrder)
                .Select(om => om.IdMalfunction)
                .ToList(),

            // Заполнение других свойств
        };



        ViewBag.SpareParts = await sparePartRepo.GetAllAsync();
        ViewBag.Works = await workRepo.GetAllAsync();
        ViewBag.Malfunctions = await malfunctionRepo.GetAllAsync();
        ViewBag.Masters = await masterRepo.GetAllAsync();

        return View(model);
    }

    // POST: Order/Edit/5
    [HttpPost]
    public async Task<IActionResult> Edit(int id, OrderViewModel model)
    {
        if (ModelState.IsValid)
        {
            var orderRepo = new OrderRepository();

            var order = new Order
            {
                IdOrder = model.IdOrder,
                IdMaster = model.IdMaster,
                // Заполнение других свойств
            };

            await orderRepo.UpdateAsync(order);
            await orderRepo.UpdateOrderDetails(order.IdOrder, model.SelectedSpareParts, model.SelectedWorks, model.SelectedMalfunctions);

            return RedirectToAction("Index");
        }

        var sparePartRepo = new SparePartRepository();
        var workRepo = new WorkRepository();
        var malfunctionRepo = new MalfunctionRepository();
        var masterRepo = new MasterRepository();

        ViewBag.SpareParts = await sparePartRepo.GetAllAsync();
        ViewBag.Works = await workRepo.GetAllAsync();
        ViewBag.Malfunctions = await malfunctionRepo.GetAllAsync();
        ViewBag.Masters = await masterRepo.GetAllAsync();

        return View(model);
    }

    // GET: Order/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Enter Delete method for Order ID {id}.");

        var orderRepo = new OrderRepository();
        var order = await orderRepo.GetByIdAsync(id);
        if (order == null)
        {
            _logger.LogWarning($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Order with ID {id} not found.");
            return RedirectToAction("Index"); // Переадресация на страницу списка заказов, если заказ не найден
        }

        return View(order);
    }

    // POST: Order/Delete/5
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        _logger.LogInformation($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Enter DeleteConfirmed method for Order ID {id}.");

        var orderRepo = new OrderRepository();
        var order = await orderRepo.GetByIdAsync(id);

        if (order != null)
        {
            _logger.LogInformation($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Deleting order with ID {id} and its related details.");
            await orderRepo.DeleteOrderDetails(order.IdOrder);
            await orderRepo.DeleteAsync(order);
            _logger.LogInformation($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Order with ID {id} deleted successfully.");
            return RedirectToAction("Index"); // Переадресация на страницу списка заказов после удаления
        }

        _logger.LogWarning($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Order with ID {id} not found for deletion.");
        return RedirectToAction("Index"); // Переадресация на страницу списка заказов, если заказ не найден для удаления
    }
}
