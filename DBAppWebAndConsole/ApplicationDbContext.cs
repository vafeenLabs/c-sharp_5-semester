using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Model>? Models { get; set; }
    public DbSet<BodyType>? BodyTypes { get; set; }
    public DbSet<Mark>? Marks { get; set; }
    public DbSet<Car>? Cars { get; set; }
    public DbSet<Person>? Persons { get; set; }
    public DbSet<Master>? Masters { get; set; }
    public DbSet<Order>? Orders { get; set; }
    public DbSet<Manager>? Managers { get; set; }
    public DbSet<Provider>? Providers { get; set; }
    public DbSet<SparePart>? SpareParts { get; set; }
    public DbSet<Work>? Works { get; set; }
    public DbSet<Malfunction>? Malfunctions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Order>()
            .HasMany(o => o.SpareParts)
            .WithMany(sp => sp.Orders);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.Malfunctions)
            .WithMany(m => m.Orders);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.Works)
            .WithMany(w => w.Orders);

    }
}
