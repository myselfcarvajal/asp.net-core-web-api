using AwpaAcademic.Api.Data;
using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Repositories.Contracts;

public interface IUserRepository : IBaseRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<List<Publicacion>> GetPublicacionesByUserIdAsync(int id);
    Task<User> AddUserAsync(User user);
    Task DeleteUserAsync(User user);
    Task<bool> EditUserAsync(int Id, User user);
}
