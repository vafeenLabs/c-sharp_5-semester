using Microsoft.EntityFrameworkCore;


public class MalfunctionRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(Malfunction malfunction)
    {
        await _context.Malfunctions.AddAsync(malfunction);
        await _context.SaveChangesAsync();
    }

    public async Task<Malfunction> GetAsync(int id) => await _context.Malfunctions.FindAsync(id);

    public async Task<List<Malfunction>> GetAllAsync() => await _context.Malfunctions.ToListAsync();

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