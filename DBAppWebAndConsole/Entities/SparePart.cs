using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


public class SparePart
{
    [Key]
    public int IdSparePart { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public ICollection<Order> Orders { get; set; } = [];
}