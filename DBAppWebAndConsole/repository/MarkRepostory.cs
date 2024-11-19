using Microsoft.EntityFrameworkCore;

public class MarkRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(Mark mark)
    {
        await _context.Marks.AddAsync(mark);
        await _context.SaveChangesAsync();
    }

    public async Task<Mark> GetAsync(int id) => await _context.Marks.FindAsync(id);

    public async Task<List<Mark>> GetAllAsync() => await _context.Marks.ToListAsync();

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
