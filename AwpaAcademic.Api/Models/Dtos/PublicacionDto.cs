using System.ComponentModel.DataAnnotations;

namespace AwpaAcademic.Api.Models.Dtos;

public record class PublicacionDto(
    Guid IdPublicacion,
    string Titulo,
    string Autor,
    string Descripcion,
    string Url,
    int UserId,
    // User User,
    string CodigoFacultad,
    // Facultad? Facultad,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record class PublicacionesDto(
    Guid IdPublicacion,
    string Titulo,
    string Autor,
    string Descripcion,
    string Url,
    // int UserId,
    UserDetailsInPublicacionDto? User,
    // string CodigoFacultad ,
    FacultadDto? Facultad,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record class UserDetailsInPublicacionDto(
    int Id,
    string Email,
    string Nombre,
    string Apellido,
    string Codigofacultad
);

public record class CreatePublicacionDto(
    [Required][StringLength(100)] string Titulo,
    [Required][StringLength(150)] string Autor,
    [Required][StringLength(500)] string Descripcion,
    [Required][StringLength(200)][Url] string Url,
    [Range(1, int.MaxValue, ErrorMessage = "The UserId field is required. Value must be between {1} and {2}.")]
    [Required] int UserId,
    [Required] string CodigoFacultad
);

public record class UpdatePublicacionDto(
    [Required][StringLength(100)] string Titulo,
    [Required][StringLength(150)] string Autor,
    [Required][StringLength(500)] string Descripcion,
    [Required][StringLength(200)][Url] string Url,
    [Required] int UserId,
    [Required] string CodigoFacultad
);