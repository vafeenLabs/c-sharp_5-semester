using Microsoft.EntityFrameworkCore;

public class CarRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(Car car)
    {
        await _context.Cars.AddAsync(car);
        await _context.SaveChangesAsync();
    }

    public async Task<Car> GetAsync(string number) => await _context.Cars.FindAsync(number);

    public async Task<List<Car>> GetAllAsync() => await _context.Cars.ToListAsync();

    public async Task UpdateAsync(Car car)
    {
        _context.Cars.Update(car);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string number)
    {
        var car = await GetAsync(number);
        if (car != null)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }
    }
}
