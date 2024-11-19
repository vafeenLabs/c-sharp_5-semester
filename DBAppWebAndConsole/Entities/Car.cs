using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


public class Car
{
    [Key]
    public string? Number { get; set; }
    public int? MarkId { get; set; }
    public int? TypeId { get; set; }
    public int? ModelId { get; set; }
    public int? PersonId { get; set; }

    public virtual Mark? Mark { get; set; }
    public virtual BodyType? BodyType { get; set; }
    public virtual Model? Model { get; set; }
    public virtual Person? Person { get; set; }
}