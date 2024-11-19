
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


public class Model
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
}