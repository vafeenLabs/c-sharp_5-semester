using Microsoft.EntityFrameworkCore;

public class ModelRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(Model model)
    {
        await _context.Models.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task<Model> GetAsync(int id) => await _context.Models.FindAsync(id);

    public async Task<List<Model>> GetAllAsync() => await _context.Models.ToListAsync();

    public async Task UpdateAsync(Model model)
    {
        _context.Models.Update(model);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var model = await GetAsync(id);
        if (model != null)
        {
            _context.Models.Remove(model);
            await _context.SaveChangesAsync();
        }
    }
}
