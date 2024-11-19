
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


public class Master
{
    [Key]
    public int IdMaster { get; set; }
    public DateTime? Date { get; set; }
    public string? Specialization { get; set; }
    public int? Experience { get; set; }
    public float? WorkRate { get; set; }
    public int PersonId { get; set; }

    public virtual Person? Person { get; set; }
}