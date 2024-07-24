using AwpaAcademic.Api.Data;
using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Repositories.Contracts;

public interface IUserRepository : IBaseRepository
{
    public List<User> GetAll();
    public User? GetById(int id);
    public User AddUser(User user);
    public void DeleteUser(User user);
    bool EditUser(int Id, User user);
}
