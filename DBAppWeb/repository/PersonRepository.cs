using Microsoft.EntityFrameworkCore;

public class PersonRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task AddAsync(Person person)
    {
        await _context.Persons.AddAsync(person);
        await _context.SaveChangesAsync();
    }

    public async Task<Person> GetAsync(int id) => await _context.Persons.FindAsync(id);

    public async Task<List<Person>> GetAllAsync() => await _context.Persons.ToListAsync();

    public async Task UpdateAsync(Person person)
    {
        _context.Persons.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var person = await GetAsync(id);
        if (person != null)
        {
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }
    }
}
