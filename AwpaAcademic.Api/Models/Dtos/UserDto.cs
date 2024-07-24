namespace AwpaAcademic.Api.Models.Dtos;

public record class UserDto(
    int Id,
    string Email,
    string Passwd,
    string Nombre,
    string Apellido,
    int RoleId,
    string Codigofacultad,
    DateTime CreatedAd,
    DateTime UpdatedAt
);
