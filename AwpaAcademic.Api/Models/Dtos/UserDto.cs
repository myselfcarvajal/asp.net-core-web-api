using System.ComponentModel.DataAnnotations;

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

public record class CreateUserDto
(
    [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")] 
    int Id,
    [Required][EmailAddress][StringLength(256)] string Email,
    [Required][StringLength(150)] string Passwd,
    [Required][StringLength(80)] string Nombre,
    [Required][StringLength(80)] string Apellido,
    [Range(1, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]  
    int RoleId,
    string Codigofacultad
);

public record class UpdateUserDto
(
    [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")] 
    int Id,
    [Required][EmailAddress][StringLength(256)] string Email,
    [Required][StringLength(150)] string Passwd,
    [Required][StringLength(80)] string Nombre,
    [Required][StringLength(80)] string Apellido,
    [Range(1, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]  
    int RoleId,
    string Codigofacultad
);