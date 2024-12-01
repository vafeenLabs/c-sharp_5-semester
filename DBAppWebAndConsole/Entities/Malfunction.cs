
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


public class Malfunction
{
    [Key]
    public int IdMalfunction { get; set; }
    public string? Description { get; set; }
    public ICollection<Order> Orders { get; set; } = [];
}