using Microsoft.EntityFrameworkCore;

public class OrderRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task AddOrderDetails(int idOrder, int sparePartCode, int workId, int idMalfunction)
    {
        var order = await _context.Orders.FindAsync(idOrder);
        if (order != null)
        {
 
            if (sparePartCode != 0)
            {
                var sparePart = await _context.SpareParts.FindAsync(sparePartCode);
                if (sparePart != null)
                {
                    _context.OrderSpareParts.Add(
                        new OrderSparePart { IdOrder = idOrder, IdSparePart = sparePartCode }
                    );
                }
            }

            if (workId != 0)
            {
                var work = await _context.Works.FindAsync(workId);
                if (work != null)
                {
                    _context.OrderWorks.Add(new OrderWork { IdOrder = idOrder, IdWork = workId });
                }
            }

            if (idMalfunction != 0)
            {
                var malfunction = await _context.Malfunctions.FindAsync(idMalfunction);
                if (malfunction != null)
                {
                    _context.OrderMalfunctions.Add(
                        new OrderMalfunction { IdOrder = idOrder, IdMalfunction = idMalfunction }
                    );
                }
            }

            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context
            .Orders.Include(o => o.OrderSpareParts)
            .ThenInclude(osp => osp.SparePart)
            .Include(o => o.OrderWorks)
            .ThenInclude(ow => ow.Work)
            .Include(o => o.OrderMalfunctions)
            .ThenInclude(om => om.Malfunction)
            .ToListAsync();
    }

    public async Task<Order> GetByIdAsync(int idOrder)
    {
        return await _context
            .Orders.Include(o => o.OrderSpareParts)
            .ThenInclude(osp => osp.SparePart)
            .Include(o => o.OrderWorks)
            .ThenInclude(ow => ow.Work)
            .Include(o => o.OrderMalfunctions)
            .ThenInclude(om => om.Malfunction)
            .FirstOrDefaultAsync(o => o.IdOrder == idOrder);
    }
    public async Task UpdateOrderDetails(
        int idOrder,
        List<int> newSpareParts,
        List<int> newWorks,
        List<int> newMalfunctions
    )
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

            foreach (var sparePartCode in newSpareParts)
            {
                var sparePart = await _context.SpareParts.FindAsync(sparePartCode);
                if (sparePart != null)
                {
                    _context.OrderSpareParts.Add(
                        new OrderSparePart { IdOrder = idOrder, IdSparePart = sparePartCode }
                    );
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
                    _context.OrderMalfunctions.Add(
                        new OrderMalfunction { IdOrder = idOrder, IdMalfunction = malfunctionId }
                    );
                }
            }

            await _context.SaveChangesAsync();
        }
    }

    
    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

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
