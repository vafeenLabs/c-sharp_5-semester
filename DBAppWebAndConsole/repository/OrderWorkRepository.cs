using Microsoft.EntityFrameworkCore;

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
        return await _context.OrderWorks.FirstOrDefaultAsync(ow =>
            ow.IdOrder == orderId && ow.IdWork == workId
        );
    }

    public async Task<List<OrderWork>> GetAllAsync()
    {
        return await _context
            .OrderWorks.Include(ow => ow.Order)
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
