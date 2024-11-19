
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class Person
{
    [Key]
    public int IdPerson { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? Phone { get; set; }
}