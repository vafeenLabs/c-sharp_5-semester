using Microsoft.EntityFrameworkCore;

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
        return await _context.OrderMalfunctions.FirstOrDefaultAsync(om =>
            om.IdOrder == orderId && om.IdMalfunction == malfunctionId
        );
    }

    public async Task<List<OrderMalfunction>> GetAllAsync()
    {
        return await _context
            .OrderMalfunctions.Include(om => om.Order)
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
