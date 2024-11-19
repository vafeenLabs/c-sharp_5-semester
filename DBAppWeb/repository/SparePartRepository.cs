using Microsoft.EntityFrameworkCore;

public class SparePartRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(SparePart sparePart)
    {
        await _context.SpareParts.AddAsync(sparePart);
        await _context.SaveChangesAsync();
    }

    public async Task<SparePart> GetAsync(int code) => await _context.SpareParts.FindAsync(code);

    public async Task<List<SparePart>> GetAllAsync() => await _context.SpareParts.ToListAsync();

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
