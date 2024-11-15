using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Services.Contracts;

public interface IUserService
{
    Task<List<UsersDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);
    Task<List<PublicacionesDto>> GetPublicacionesByUserIdAsync(int id);
    Task<User> AddUserAsync(CreateUserDto createUserDto);
    Task<bool> DeleteUserAsync(int id);
    Task<bool> EditUserAsync(int id, UpdateUserDto updateUserDto);
    Task SaveChangesAsync();
}
