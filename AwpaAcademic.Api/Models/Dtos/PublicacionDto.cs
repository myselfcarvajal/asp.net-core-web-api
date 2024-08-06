using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Models.Dtos;

public record class PublicacionDto(
    // Guid IdPublicacion,
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
