using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


public class OrderMalfunction
{
    [Key]
    public int IdOrder { get; set; }

    [Key]
    public int IdMalfunction { get; set; }
    public Order? Order { get; set; }
    public Malfunction? Malfunction { get; set; }
}
