using System.ComponentModel.DataAnnotations;

namespace AwpaAcademic.Api.Models.Entities;

public class Role
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
}
