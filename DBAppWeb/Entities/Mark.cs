using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


public class Mark
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
}
