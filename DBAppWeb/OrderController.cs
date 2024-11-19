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

        return View();
    }

    // POST: Order/Create
    [HttpPost]
    public async Task<IActionResult> Create(OrderViewModel model)
    {
        if (ModelState.IsValid)
        {
            _logger.LogInformation($"Received Model: IdMaster: {model.IdMaster}, SelectedSpareParts: {string.Join(", ", model.SelectedSpareParts ?? new List<int>())}, SelectedWorks: {string.Join(", ", model.SelectedWorks ?? new List<int>())}, SelectedMalfunctions: {string.Join(", ", model.SelectedMalfunctions ?? new List<int>())}");

            var orderRepo = new OrderRepository();

            // Создание основного заказа
            var order = new Order
            {
                IdMaster = model.IdMaster
                // Заполнение других свойств заказа
            };
            await orderRepo.AddAsync(order);

            // Добавление связанных данных (списков)
            await orderRepo.UpdateOrderDetails(order.IdOrder, model.SelectedSpareParts, model.SelectedWorks, model.SelectedMalfunctions);

            return RedirectToAction("Index"); // Переадресация на список заказов
        }

        // Если модель некорректна, загрузите данные заново
        var sparePartRepo = new SparePartRepository();
        var workRepo = new WorkRepository();
        var malfunctionRepo = new MalfunctionRepository();
        var masterRepo = new MasterRepository();

        ViewBag.SpareParts = await sparePartRepo.GetAllAsync();
        ViewBag.Works = await workRepo.GetAllAsync();
        ViewBag.Malfunctions = await malfunctionRepo.GetAllAsync();
        ViewBag.Masters = await masterRepo.GetAllAsync();

        return View(model); // Вернуть модель обратно в представление
    }



    // GET: Order/Index
    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Enter Index method.");

        var orderRepo = new OrderRepository();
        var orders = await orderRepo.GetAllAsync();

        _logger.LogInformation($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Fetched {orders.Count()} orders.");
        return View(orders);
    }

    // GET: Order/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        _logger.LogInformation($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Enter Edit method for Order ID {id}.");

        var orderRepo = new OrderRepository();
        var sparePartRepo = new SparePartRepository();
        var workRepo = new WorkRepository();
        var malfunctionRepo = new MalfunctionRepository();
        var masterRepo = new MasterRepository();

        var order = await orderRepo.GetByIdAsync(id);
        if (order == null)
        {
            _logger.LogWarning($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Order with ID {id} not found.");
            return RedirectToAction("Index"); // Переадресация на страницу списка заказов, если заказ не найден
        }

        _logger.LogInformation("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Fetching data for order edit.");
        ViewBag.SpareParts = await sparePartRepo.GetAllAsync();
        ViewBag.Works = await workRepo.GetAllAsync();
        ViewBag.Malfunctions = await malfunctionRepo.GetAllAsync();
        ViewBag.Masters = await masterRepo.GetAllAsync();

        _logger.LogInformation("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Data fetched successfully.");
        return View(order);
    }

    // POST: Order/Edit/5
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Order order, int[] selectedSpareParts, int[] selectedWorks, int[] selectedMalfunctions)
    {
        _logger.LogInformation($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Enter Edit POST method for Order ID {id}.");

        var orderRepo = new OrderRepository();

        if (id != order.IdOrder)
        {
            _logger.LogWarning($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Order ID mismatch. Provided ID: {id}, Order ID: {order.IdOrder}");
            return RedirectToAction("Index"); // Переадресация на страницу списка заказов, если ID не совпадают
        }

        if (ModelState.IsValid)
        {
            _logger.LogInformation("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Model is valid. Updating order in the database.");
            await orderRepo.UpdateAsync(order);

            _logger.LogInformation("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Updating order details.");
            await orderRepo.UpdateOrderDetails(order.IdOrder, selectedSpareParts.ToList(), selectedWorks.ToList(), selectedMalfunctions.ToList());

            _logger.LogInformation("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Order updated successfully.");
            return RedirectToAction("Index"); // Переадресация на страницу списка заказов после обновления
        }

        _logger.LogWarning("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Model state is invalid.");
        return View(order);
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
