using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class OrderController : Controller
{
    private readonly CreateOrderUseCaseWeb _createOrderUseCase = new CreateOrderUseCaseWeb();
    private readonly EditOrderUseCaseWeb _editOrderUseCase = new EditOrderUseCaseWeb();
    private readonly DetailsUseCaseWeb _detailsUseCase = new DetailsUseCaseWeb();
    private readonly DeleteOrderUseCaseWeb _deleteOrderUseCase = new DeleteOrderUseCaseWeb();
    private readonly OrderRepository _orderRepository = new OrderRepository();


    [HttpGet]
    public async Task<IActionResult> Create()
    {
        try
        {
            var model = await _createOrderUseCase.Get();
            return View(model);
        }
        catch
        {
            return BadRequest("Ошибка при создании модели для заказа.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var result = await _createOrderUseCase.Post(model);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Ошибка при создании заказа.");
            return View(model);
        }
        catch
        {
            return BadRequest("Произошла ошибка при сохранении заказа.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var model = await _editOrderUseCase.Get(id);
            return model == null ? NotFound() : View(model);
        }
        catch
        {
            return BadRequest("Ошибка при получении данных для редактирования.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, OrderViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var result = await _editOrderUseCase.Post(id, model);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Ошибка при сохранении изменений.");
            return View(model);
        }
        catch
        {
            return BadRequest("Произошла ошибка при обновлении заказа.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var model = await _detailsUseCase.Get(id);
            return model == null ? NotFound() : View(model);
        }
        catch
        {
            return BadRequest("Ошибка при получении данных заказа.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmDelete(int id)
    {
        try
        {
            var model = await _deleteOrderUseCase.Get(id);
            return model == null ? NotFound() : View(model);
        }
        catch
        {
            return BadRequest("Ошибка при получении данных для удаления.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _deleteOrderUseCase.Post(id);
            if (result)
                return RedirectToAction("Index");

            return BadRequest("Ошибка при удалении заказа.");
        }
        catch
        {
            return BadRequest("Произошла ошибка при удалении заказа.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var orders = await _orderRepository.GetAllAsync();
            return View(orders);
        }
        catch
        {
            return BadRequest("Ошибка при загрузке страницы заказов.");
        }
    }
}
