using System.ComponentModel.DataAnnotations;

namespace AwpaAcademic.Api.Models.Entities;

public class Publicacion
{
    [Key]
    public Guid IdPublicacion { get; set; }
    public required string Titulo { get; set; }
    public required List<string> Autor { get; set; }
    public required string Descripcion { get; set; }
    public string? Url { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public string? CodigoFacultad { get; set; }
    public Facultad? Facultad { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}