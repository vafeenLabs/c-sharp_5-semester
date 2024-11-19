using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// Master Repository
public class MasterRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(Master master)
    {
        await _context.Masters.AddAsync(master);
        await _context.SaveChangesAsync();
    }

    public async Task<Master> GetAsync(int id) => await _context.Masters.FindAsync(id);

    public async Task<IEnumerable<Master>> GetAllAsync() => await _context.Masters.ToListAsync();

    public async Task UpdateAsync(Master master)
    {
        _context.Masters.Update(master);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var master = await GetAsync(id);
        if (master != null)
        {
            _context.Masters.Remove(master);
            await _context.SaveChangesAsync();
        }
    }
}

// Malfunction Repository
public class MalfunctionRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(Malfunction malfunction)
    {
        await _context.Malfunctions.AddAsync(malfunction);
        await _context.SaveChangesAsync();
    }

    public async Task<Malfunction> GetAsync(int id) => await _context.Malfunctions.FindAsync(id);

    public async Task<IEnumerable<Malfunction>> GetAllAsync() => await _context.Malfunctions.ToListAsync();

    public async Task UpdateAsync(Malfunction malfunction)
    {
        _context.Malfunctions.Update(malfunction);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var malfunction = await GetAsync(id);
        if (malfunction != null)
        {
            _context.Malfunctions.Remove(malfunction);
            await _context.SaveChangesAsync();
        }
    }
}

// Model Repository
public class ModelRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(Model model)
    {
        await _context.Models.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task<Model> GetAsync(int id) => await _context.Models.FindAsync(id);

    public async Task<IEnumerable<Model>> GetAllAsync() => await _context.Models.ToListAsync();

    public async Task UpdateAsync(Model model)
    {
        _context.Models.Update(model);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var model = await GetAsync(id);
        if (model != null)
        {
            _context.Models.Remove(model);
            await _context.SaveChangesAsync();
        }
    }
}

// BodyType Repository
public class BodyTypeRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(BodyType bodyType)
    {
        await _context.BodyTypes.AddAsync(bodyType);
        await _context.SaveChangesAsync();
    }

    public async Task<BodyType> GetAsync(int id) => await _context.BodyTypes.FindAsync(id);

    public async Task<IEnumerable<BodyType>> GetAllAsync() => await _context.BodyTypes.ToListAsync();

    public async Task UpdateAsync(BodyType bodyType)
    {
        _context.BodyTypes.Update(bodyType);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var bodyType = await GetAsync(id);
        if (bodyType != null)
        {
            _context.BodyTypes.Remove(bodyType);
            await _context.SaveChangesAsync();
        }
    }
}

// Mark Repository
public class MarkRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(Mark mark)
    {
        await _context.Marks.AddAsync(mark);
        await _context.SaveChangesAsync();
    }

    public async Task<Mark> GetAsync(int id) => await _context.Marks.FindAsync(id);

    public async Task<IEnumerable<Mark>> GetAllAsync() => await _context.Marks.ToListAsync();

    public async Task UpdateAsync(Mark mark)
    {
        _context.Marks.Update(mark);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var mark = await GetAsync(id);
        if (mark != null)
        {
            _context.Marks.Remove(mark);
            await _context.SaveChangesAsync();
        }
    }
}

// Car Repository
public class CarRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(Car car)
    {
        await _context.Cars.AddAsync(car);
        await _context.SaveChangesAsync();
    }

    public async Task<Car> GetAsync(string number) => await _context.Cars.FindAsync(number);

    public async Task<IEnumerable<Car>> GetAllAsync() => await _context.Cars.ToListAsync();

    public async Task UpdateAsync(Car car)
    {
        _context.Cars.Update(car);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string number)
    {
        var car = await GetAsync(number);
        if (car != null)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }
    }
}

// Person Repository
public class PersonRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(Person person)
    {
        await _context.Persons.AddAsync(person);
        await _context.SaveChangesAsync();
    }

    public async Task<Person> GetAsync(int id) => await _context.Persons.FindAsync(id);

    public async Task<IEnumerable<Person>> GetAllAsync() => await _context.Persons.ToListAsync();

    public async Task UpdateAsync(Person person)
    {
        _context.Persons.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var person = await GetAsync(id);
        if (person != null)
        {
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }
    }
}

// SparePart Repository
public class SparePartRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(SparePart sparePart)
    {
        await _context.SpareParts.AddAsync(sparePart);
        await _context.SaveChangesAsync();
    }

    public async Task<SparePart> GetAsync(int code) => await _context.SpareParts.FindAsync(code);

    public async Task<IEnumerable<SparePart>> GetAllAsync() => await _context.SpareParts.ToListAsync();

    public async Task UpdateAsync(SparePart sparePart)
    {
        _context.SpareParts.Update(sparePart);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int code)
    {
        var sparePart = await GetAsync(code);
        if (sparePart != null)
        {
            _context.SpareParts.Remove(sparePart);
            await _context.SaveChangesAsync();
        }
    }
}

// Work Repository
public class WorkRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(Work work)
    {
        await _context.Works.AddAsync(work);
        await _context.SaveChangesAsync();
    }

    public async Task<Work> GetAsync(int id) => await _context.Works.FindAsync(id);

    public async Task<IEnumerable<Work>> GetAllAsync() => await _context.Works.ToListAsync();

    public async Task UpdateAsync(Work work)
    {
        _context.Works.Update(work);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var work = await GetAsync(id);
        if (work != null)
        {
            _context.Works.Remove(work);
            await _context.SaveChangesAsync();
        }
    }
}

// Order Repository

public class OrderRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    // Метод для добавления заказа с связанными сущностями
    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    // Метод для добавления деталей заказа (запасные части, работы, неисправности)
    public async Task AddOrderDetails(int idOrder, int sparePartCode, int workId, int idMalfunction)
    {
        var order = await _context.Orders.FindAsync(idOrder);
        if (order != null)
        {
            // Добавляем запасные части
            if (sparePartCode != 0)
            {
                var sparePart = await _context.SpareParts.FindAsync(sparePartCode);
                if (sparePart != null)
                {
                    _context.OrderSpareParts.Add(new OrderSparePart { IdOrder = idOrder, IdSparePart = sparePartCode });
                }
            }

            // Добавляем работы
            if (workId != 0)
            {
                var work = await _context.Works.FindAsync(workId);
                if (work != null)
                {
                    _context.OrderWorks.Add(new OrderWork { IdOrder = idOrder, IdWork = workId });
                }
            }

            // Добавляем неисправности
            if (idMalfunction != 0)
            {
                var malfunction = await _context.Malfunctions.FindAsync(idMalfunction);
                if (malfunction != null)
                {
                    _context.OrderMalfunctions.Add(new OrderMalfunction { IdOrder = idOrder, IdMalfunction = idMalfunction });
                }
            }

            await _context.SaveChangesAsync();
        }
    }

    // Получение всех заказов с их связями
    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderSpareParts)
                .ThenInclude(osp => osp.SparePart)
            .Include(o => o.OrderWorks)
                .ThenInclude(ow => ow.Work)
            .Include(o => o.OrderMalfunctions)
                .ThenInclude(om => om.Malfunction)
            .ToListAsync();
    }

    // Получение заказа по ID
    public async Task<Order> GetByIdAsync(int idOrder)
    {
        return await _context.Orders
            .Include(o => o.OrderSpareParts)
                .ThenInclude(osp => osp.SparePart)
            .Include(o => o.OrderWorks)
                .ThenInclude(ow => ow.Work)
            .Include(o => o.OrderMalfunctions)
                .ThenInclude(om => om.Malfunction)
            .FirstOrDefaultAsync(o => o.IdOrder == idOrder);
    }

    // Обновление деталей заказа (запасные части, работы, неисправности)
    public async Task UpdateOrderDetails(int idOrder, List<int> newSpareParts, List<int> newWorks, List<int> newMalfunctions)
    {
        var order = await _context.Orders.FindAsync(idOrder);
        if (order != null)
        {
            // Удаляем старые детали
            var currentSpareParts = _context.OrderSpareParts.Where(osp => osp.IdOrder == idOrder);
            _context.OrderSpareParts.RemoveRange(currentSpareParts);

            var currentWorks = _context.OrderWorks.Where(ow => ow.IdOrder == idOrder);
            _context.OrderWorks.RemoveRange(currentWorks);

            var currentMalfunctions = _context.OrderMalfunctions.Where(om => om.IdOrder == idOrder);
            _context.OrderMalfunctions.RemoveRange(currentMalfunctions);

            // Добавляем новые детали
            foreach (var sparePartCode in newSpareParts)
            {
                var sparePart = await _context.SpareParts.FindAsync(sparePartCode);
                if (sparePart != null)
                {
                    _context.OrderSpareParts.Add(new OrderSparePart { IdOrder = idOrder, IdSparePart = sparePartCode });
                }
            }

            foreach (var workId in newWorks)
            {
                var work = await _context.Works.FindAsync(workId);
                if (work != null)
                {
                    _context.OrderWorks.Add(new OrderWork { IdOrder = idOrder, IdWork = workId });
                }
            }

            foreach (var malfunctionId in newMalfunctions)
            {
                var malfunction = await _context.Malfunctions.FindAsync(malfunctionId);
                if (malfunction != null)
                {
                    _context.OrderMalfunctions.Add(new OrderMalfunction { IdOrder = idOrder, IdMalfunction = malfunctionId });
                }
            }

            await _context.SaveChangesAsync();
        }
    }

    // Обновление заказа
    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    // Удаление заказа по ID
    public async Task DeleteAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    // Удаление деталей заказа
    public async Task DeleteOrderDetails(int idOrder)
    {
        var order = await _context.Orders.FindAsync(idOrder);
        if (order != null)
        {
            var currentSpareParts = _context.OrderSpareParts.Where(osp => osp.IdOrder == idOrder);
            _context.OrderSpareParts.RemoveRange(currentSpareParts);

            var currentWorks = _context.OrderWorks.Where(ow => ow.IdOrder == idOrder);
            _context.OrderWorks.RemoveRange(currentWorks);

            var currentMalfunctions = _context.OrderMalfunctions.Where(om => om.IdOrder == idOrder);
            _context.OrderMalfunctions.RemoveRange(currentMalfunctions);

            await _context.SaveChangesAsync();
        }
    }
}




// OrderSparePart Repository
public class OrderSparePartRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(OrderSparePart orderSparePart)
    {
        _context.OrderSpareParts.Add(orderSparePart);
        await _context.SaveChangesAsync();
    }

    public async Task<OrderSparePart> GetAsync(int orderId, int sparePartId)
    {
        return await _context.OrderSpareParts
            .FirstOrDefaultAsync(osp => osp.IdOrder == orderId && osp.IdSparePart == sparePartId);
    }

    public async Task<IEnumerable<OrderSparePart>> GetAllAsync()
    {
        return await _context.OrderSpareParts
            .Include(osp => osp.Order)
            .Include(osp => osp.SparePart)
            .ToListAsync();
    }

    public async Task UpdateAsync(OrderSparePart orderSparePart)
    {
        _context.OrderSpareParts.Update(orderSparePart);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int orderId, int sparePartId)
    {
        var orderSparePart = await GetAsync(orderId, sparePartId);
        if (orderSparePart != null)
        {
            _context.OrderSpareParts.Remove(orderSparePart);
            await _context.SaveChangesAsync();
        }
    }
}

// OrderWork Repository
public class OrderWorkRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(OrderWork orderWork)
    {
        _context.OrderWorks.Add(orderWork);
        await _context.SaveChangesAsync();
    }

    public async Task<OrderWork> GetAsync(int orderId, int workId)
    {
        return await _context.OrderWorks
            .FirstOrDefaultAsync(ow => ow.IdOrder == orderId && ow.IdWork == workId);
    }

    public async Task<IEnumerable<OrderWork>> GetAllAsync()
    {
        return await _context.OrderWorks
            .Include(ow => ow.Order)
            .Include(ow => ow.Work)
            .ToListAsync();
    }

    public async Task UpdateAsync(OrderWork orderWork)
    {
        _context.OrderWorks.Update(orderWork);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int orderId, int workId)
    {
        var orderWork = await GetAsync(orderId, workId);
        if (orderWork != null)
        {
            _context.OrderWorks.Remove(orderWork);
            await _context.SaveChangesAsync();
        }
    }
}

// OrderMalfunction Repository
public class OrderMalfunctionRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(OrderMalfunction orderMalfunction)
    {
        _context.OrderMalfunctions.Add(orderMalfunction);
        await _context.SaveChangesAsync();
    }

    public async Task<OrderMalfunction> GetAsync(int orderId, int malfunctionId)
    {
        return await _context.OrderMalfunctions
            .FirstOrDefaultAsync(om => om.IdOrder == orderId && om.IdMalfunction == malfunctionId);
    }

    public async Task<IEnumerable<OrderMalfunction>> GetAllAsync()
    {
        return await _context.OrderMalfunctions
            .Include(om => om.Order)
            .Include(om => om.Malfunction)
            .ToListAsync();
    }

    public async Task UpdateAsync(OrderMalfunction orderMalfunction)
    {
        _context.OrderMalfunctions.Update(orderMalfunction);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int orderId, int malfunctionId)
    {
        var orderMalfunction = await GetAsync(orderId, malfunctionId);
        if (orderMalfunction != null)
        {
            _context.OrderMalfunctions.Remove(orderMalfunction);
            await _context.SaveChangesAsync();
        }
    }
}
