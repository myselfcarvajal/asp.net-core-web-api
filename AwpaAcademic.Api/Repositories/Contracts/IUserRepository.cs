using AwpaAcademic.Api.Data;
using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Repositories.Contracts;

public interface IUserRepository : IBaseRepository
{
    Task<List<User>> GetAllAsync();
    Task<bool> ExistsAsync(int id);
    Task<bool> ExistsWithFacultad(string codigoFacultad);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<List<Publicacion>> GetPublicacionesByUserIdAsync(int id);
    Task<User> AddUserAsync(User user);
    Task DeleteUserAsync(User user);
    Task<bool> EditUserAsync(int Id, User user);
}
