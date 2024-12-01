
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


public class OrderSparePart
{
    [Key]
    public int IdOrder { get; set; }

    [Key]
    public int IdSparePart { get; set; }
    public Order? Order { get; set; }
    public SparePart? SparePart { get; set; }
}