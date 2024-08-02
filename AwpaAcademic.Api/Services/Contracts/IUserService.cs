using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Services.Contracts;

public interface IUserService
{
    Task<List<UsersDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);

    Task<User> AddUserAsync(UserDto userDto);
    Task<bool> DeleteUserAsync(int id);
    Task<bool> EditUserAsync(int Id, UserDto userDto);
    Task SaveChangesAsync();
}
