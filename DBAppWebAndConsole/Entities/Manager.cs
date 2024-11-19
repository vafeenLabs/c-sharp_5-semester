using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


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