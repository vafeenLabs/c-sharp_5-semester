using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Model> Models { get; set; }
    public DbSet<BodyType> BodyTypes { get; set; }
    public DbSet<Mark> Marks { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Master> Masters { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Provider> Providers { get; set; }
    public DbSet<SparePart> SpareParts { get; set; }
    public DbSet<Work> Works { get; set; }
    public DbSet<Malfunction> Malfunctions { get; set; }
    public DbSet<OrderMalfunction> OrderMalfunctions { get; set; }
    public DbSet<OrderSparePart> OrderSpareParts { get; set; }
    public DbSet<OrderWork> OrderWorks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Множественные связи через промежуточные таблицы
        modelBuilder.Entity<OrderMalfunction>()
            .HasKey(om => new { om.IdOrder, om.IdMalfunction });

        modelBuilder.Entity<OrderSparePart>()
            .HasKey(osp => new { osp.IdOrder, osp.IdSparePart });

        modelBuilder.Entity<OrderWork>()
            .HasKey(ow => new { ow.IdOrder, ow.IdWork });

        // Дополнительные настройки для сущностей, например, уникальные ограничения
        // modelBuilder.Entity<OrderSparePart>()
        //     .HasOne(osp => osp.Order)
        //     .WithMany(o => o.OrderSpareParts)
        //     .HasForeignKey(osp => osp.IdOrder);

        // modelBuilder.Entity<OrderWork>()
        //     .HasOne(ow => ow.Order)
        //     .WithMany(o => o.OrderWorks)
        //     .HasForeignKey(ow => ow.IdOrder);

        // modelBuilder.Entity<OrderMalfunction>()
        //     .HasOne(om => om.Order)
        //     .WithMany(o => o.OrderMalfunctions)
        //     .HasForeignKey(om => om.IdOrder);
    }

    // Use SQLite for local database
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }
}

public class Model
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class BodyType
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class Mark
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class Car
{
    [Key]
    public string? Number { get; set; }
    public int? MarkId { get; set; }
    public int? TypeId { get; set; }
    public int? ModelId { get; set; }
    public int? PersonId { get; set; }

    public virtual Mark? Mark { get; set; }
    public virtual BodyType? BodyType { get; set; }
    public virtual Model? Model { get; set; }
    public virtual Person? Person { get; set; }
}

public class Person
{
    [Key]
    public int IdPerson { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? Phone { get; set; }
}

public class Master
{
    [Key]
    public int IdMaster { get; set; }
    public DateTime? Date { get; set; }
    public string? Specialization { get; set; }
    public int? Experience { get; set; }
    public float? WorkRate { get; set; }
    public int PersonId { get; set; }

    public virtual Person Person { get; set; }
}



public class Manager
{
    [Key]
    public int Id { get; set; }
    public int? ProviderId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? Phone { get; set; }
}

public class Provider
{
    [Key]
    public int Id { get; set; }
    public string? NameProvider { get; set; }
    public string? Address { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? Phone { get; set; }
}


public class Order
{
    [Key]
    public int IdOrder { get; set; }
    public int IdMaster { get; set; }
    public DateTime OrderDate { get; set; }
    public Master? Master { get; set; }
    public string? Comment { get; set; }
    public ICollection<OrderSparePart>? OrderSpareParts { get; set; }
    public ICollection<OrderMalfunction>? OrderMalfunctions { get; set; }
    public ICollection<OrderWork>? OrderWorks { get; set; }
}

public class OrderSparePart
{
    [Key]
    public int IdOrder { get; set; }
    [Key]
    public int IdSparePart { get; set; }
    public Order? Order { get; set; }
    public SparePart? SparePart { get; set; }
}

public class OrderMalfunction
{

    [Key]
    public int IdOrder { get; set; }
    [Key]
    public int IdMalfunction { get; set; }
    public Order? Order { get; set; }
    public Malfunction? Malfunction { get; set; }
}

public class OrderWork
{
    [Key]
    public int IdOrder { get; set; }
    [Key]
    public int IdWork { get; set; }
    public Order? Order { get; set; }
    public Work? Work { get; set; }
}

public class SparePart
{
    [Key]
    public int IdSparePart { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}

public class Malfunction
{
    [Key]
    public int IdMalfunction { get; set; }
    public string? Description { get; set; }
}

public class Work
{
    [Key]
    public int IdWork { get; set; }
    public string? WorkDescription { get; set; }
    public decimal Price { get; set; }
}




public class OrderViewModel
{
    public int IdMaster { get; set; }
    public List<int> SelectedSpareParts { get; set; } = new();
    public List<int> SelectedWorks { get; set; } = new();
    public List<int> SelectedMalfunctions { get; set; } = new();

    public override string ToString()
    {
        return $"{IdMaster}, " +
               $"[{string.Join(", ", SelectedSpareParts)}], " +
               $"[{string.Join(", ", SelectedWorks)}], " +
               $"[{string.Join(", ", SelectedMalfunctions)}]";
    }
}