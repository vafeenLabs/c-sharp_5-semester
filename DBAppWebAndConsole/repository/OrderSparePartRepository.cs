using Microsoft.EntityFrameworkCore;

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
        return await _context.OrderSpareParts.FirstOrDefaultAsync(osp =>
            osp.IdOrder == orderId && osp.IdSparePart == sparePartId
        );
    }

    public async Task<List<OrderSparePart>> GetAllAsync()
    {
        return await _context
            .OrderSpareParts.Include(osp => osp.Order)
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
