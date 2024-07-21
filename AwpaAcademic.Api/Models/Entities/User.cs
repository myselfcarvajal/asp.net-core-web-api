using System.ComponentModel.DataAnnotations;

namespace AwpaAcademic.Api.Models.Entities;

public class User
{
    [Key]
    public int Id { get; set; }

    [EmailAddress]
    public required string Email { get; set; }
    public required string Passwd { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }

    public List<Publicacion>? Publicaciones { get; set; }

    public int RoleId { get; set; }
    public Role? Role { get; set; }

    public string? Codigofacultad { get; set; }
    public Facultad? Facultad { get; set; }

    public DateTime CreatedAd { get; set; }
    public DateTime UpdatedAt { get; set; }
}
