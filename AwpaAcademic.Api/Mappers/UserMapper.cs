using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Mappers;

public class UserMapper : IUserMapper
{
    public User MapToUser(UserDto userDto)
    {

        return new User()
        {
            Id = userDto.Id,
            Email = userDto.Email,
            Passwd = userDto.Passwd,
            Nombre = userDto.Nombre,
            Apellido = userDto.Apellido,
            RoleId = userDto.RoleId,
            Codigofacultad = userDto.Codigofacultad,
            CreatedAd = userDto.CreatedAd,
            UpdatedAt = userDto.UpdatedAt,
        };

    }

    public UserDto MapToUserDto(User user)
    {
        return new(
            user.Id,
            user.Email,
            user.Passwd,
            user.Nombre,
            user.Apellido,
            user.RoleId,
            user.Codigofacultad ?? string.Empty,
            user.CreatedAd,
            user.UpdatedAt
        );
    }

    public UsersDto MapToUsersDto(User user)
    {
        return new(
            user.Id,
            user.Email,
            user.Passwd,
            user.Nombre,
            user.Apellido,
            user.RoleId,
            user.Facultad == null ? null : new FacultadDto(
                user.Facultad.CodigoFacultad,
                user.Facultad.NombreFacultad
            ),
            user.CreatedAd,
            user.UpdatedAt
        );
    }
}
