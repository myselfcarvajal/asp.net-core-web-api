using AwpaAcademic.Api.Data;
using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Repositories.Contracts;

public interface IAuthRepository : IBaseRepository
{
    Task<User> RegisterUserAsync(User user);
    Task<User?> GetByEmailAsync(string email);
}
