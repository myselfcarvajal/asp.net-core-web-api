using System.ComponentModel.DataAnnotations;

namespace AwpaAcademic.Api.Models.Dtos;

public record class FacultadDto(
    [Required] [StringLength(8)] string CodigoFacultad,
    [Required] [StringLength(60)] string NombreFacultad
);
