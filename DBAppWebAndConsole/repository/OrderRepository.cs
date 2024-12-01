using Microsoft.EntityFrameworkCore;

public class OrderRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }


public async Task<List<Order>> GetAllAsync()
{
    return await _context
        .Orders
        .Include(o => o.Master) // Подгружаем Master, если нужно
        .Include(o => o.SpareParts)
        .Include(o => o.Malfunctions)
        .Include(o => o.Works)
        .ToListAsync();
}

public async Task<Order?> GetByIdAsync(int idOrder)
{
    return await _context
        .Orders
        .Include(o => o.Master) // Подгружаем Master, если нужно
        .Include(o => o.SpareParts)
        .Include(o => o.Malfunctions)
        .Include(o => o.Works)
        .FirstOrDefaultAsync(o => o.IdOrder == idOrder);
}

    public async Task UpdateOrderDetails(
        int idOrder,
        List<SparePart> newSpareParts,
        List<Work> newWorks,
        List<Malfunction> newMalfunctions
    )
    {
        var order = await GetByIdAsync(idOrder);
        if (order != null)
        {
            
            await DeleteOrderDetails(idOrder);
           
            foreach (var sparePart in newSpareParts)
            {
             order.SpareParts.Add(sparePart);    
            }

            foreach (var work in newWorks)
            {
                   order.Works.Add(work);
                
            }

            foreach (var malfunction in newMalfunctions)
            {
                   order.Malfunctions.Add(malfunction);   
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
            order.SpareParts.Clear();
            order.Works.Clear();
            order.Malfunctions.Clear();
        }
    }
}
