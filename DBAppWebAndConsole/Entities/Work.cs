
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class Work
{
    [Key]
    public int IdWork { get; set; }
    public string? WorkDescription { get; set; }
    public decimal Price { get; set; }
    public ICollection<Order> Orders { get; set; } = [];
}