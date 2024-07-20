using System.ComponentModel.DataAnnotations;

namespace AwpaAcademic.Api.Models.Entities;

public class Facultad
{
    [Key]
    public required string CodigoFacultad { get; set; }
    public required string NombreFacultad { get; set; }
    public List<Publicacion>? Publicaciones { get; set; }
}
