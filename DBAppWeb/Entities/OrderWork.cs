using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


public class OrderWork
{
    [Key]
    public int IdOrder { get; set; }

    [Key]
    public int IdWork { get; set; }
    public Order? Order { get; set; }
    public Work? Work { get; set; }
}
