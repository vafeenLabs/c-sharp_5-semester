using Microsoft.AspNetCore.Mvc;

public class OrderController : Controller
{
    private readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger)
    {
        _logger = logger;
    }

    // View
    public async Task<IActionResult> Index()
    {
        var orderRepo = new OrderRepository();
        var orders = await orderRepo.GetAllAsync();
        return View(orders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var useCase = new DetailsUseCaseWeb();
        var model = await useCase.Get(id);

        if (model == null)
            return RedirectToAction("Index");

        return View(model);
    }
    // Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var useCase = new CreateOrderUseCaseWeb();
        var model = await useCase.Get();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderViewModel model)
    {
        var useCase = new CreateOrderUseCaseWeb();

        if (await useCase.Post(model))
            return RedirectToAction("Index");

        // Если модель некорректна, перезагружаем данные
        model = await useCase.Get();
        return View(model);
    }

   

    // Update
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var useCase = new EditOrderUseCaseWeb();
        var model = await useCase.Get(id);

        if (model == null)
            return RedirectToAction("Index");

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, OrderViewModel model)
    {
        var useCase = new EditOrderUseCaseWeb();

        if (await useCase.Post(id, model))
            return RedirectToAction("Index");

        // Если модель некорректна, перезагружаем данные
        model = await useCase.Get(id);
        return View(model);
    }

    // Delete
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var useCase = new DeleteOrderUseCaseWeb();
        var order = await useCase.Get(id);

        if (order == null)
            return RedirectToAction("Index");

        return View(order);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var useCase = new DeleteOrderUseCaseWeb();

        if (await useCase.Post(id))
            return RedirectToAction("Index");

        return RedirectToAction("Index"); // Если удаление не удалось
    }
}
