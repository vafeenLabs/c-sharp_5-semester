using Microsoft.EntityFrameworkCore;

public class MasterRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(Master master)
    {
        await _context.Masters.AddAsync(master);
        await _context.SaveChangesAsync();
    }

    public async Task<Master> GetAsync(int id) => await _context.Masters.FindAsync(id);

    public async Task<List<Master>> GetAllAsync() => await _context.Masters.ToListAsync();

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
