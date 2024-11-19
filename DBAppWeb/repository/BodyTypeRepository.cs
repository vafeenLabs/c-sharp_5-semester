using Microsoft.EntityFrameworkCore;

public class BodyTypeRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(BodyType bodyType)
    {
        await _context.BodyTypes.AddAsync(bodyType);
        await _context.SaveChangesAsync();
    }

    public async Task<BodyType> GetAsync(int id) => await _context.BodyTypes.FindAsync(id);

    public async Task<List<BodyType>> GetAllAsync() => await _context.BodyTypes.ToListAsync();

    public async Task UpdateAsync(BodyType bodyType)
    {
        _context.BodyTypes.Update(bodyType);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var bodyType = await GetAsync(id);
        if (bodyType != null)
        {
            _context.BodyTypes.Remove(bodyType);
            await _context.SaveChangesAsync();
        }
    }
}
