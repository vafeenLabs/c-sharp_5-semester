using Microsoft.EntityFrameworkCore;

public class WorkRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(Work work)
    {
        await _context.Works.AddAsync(work);
        await _context.SaveChangesAsync();
    }

    public async Task<Work> GetAsync(int id) => await _context.Works.FindAsync(id);

    public async Task<List<Work>> GetAllAsync() => await _context.Works.ToListAsync();

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
