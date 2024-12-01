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
    public DbSet<OrderMalfunction>? OrderMalfunctions { get; set; }
    public DbSet<OrderSparePart>? OrderSpareParts { get; set; }
    public DbSet<OrderWork>? OrderWorks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderMalfunction>().HasKey(om => new { om.IdOrder, om.IdMalfunction });

        modelBuilder.Entity<OrderSparePart>().HasKey(osp => new { osp.IdOrder, osp.IdSparePart });

        modelBuilder.Entity<OrderWork>().HasKey(ow => new { ow.IdOrder, ow.IdWork });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }
}