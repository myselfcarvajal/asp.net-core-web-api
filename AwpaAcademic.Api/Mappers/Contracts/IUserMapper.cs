using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Mappers.Contracts;

public interface IUserMapper
{
    public User MapToUser(UserDto userDto);
    public UserDto MapToUserDto(User user);
}
