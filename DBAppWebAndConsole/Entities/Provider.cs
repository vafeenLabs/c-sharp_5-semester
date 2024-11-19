
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


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