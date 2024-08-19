using System.ComponentModel.DataAnnotations;

namespace AwpaAcademic.Api.Models.Dtos;

public record class FacultadDto(
    string CodigoFacultad,
    string NombreFacultad
);

public record class CreateFacultadDto(
    [Required][StringLength(8)] string CodigoFacultad,
    [Required][StringLength(60)] string NombreFacultad
);

public record class UpdateFacultadDto(
    [Required][StringLength(60)] string NombreFacultad
);
