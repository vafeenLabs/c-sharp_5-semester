using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


public class BodyType
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
}