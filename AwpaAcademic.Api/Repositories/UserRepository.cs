using AwpaAcademic.Api.Data;
using AwpaAcademic.Api.Models.Entities;
using AwpaAcademic.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AwpaAcademic.Api.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    private AwpaAcademicDbContext _awpaAcademicDbContext;
    public UserRepository(AwpaAcademicDbContext awpaAcademicDbContext) : base(awpaAcademicDbContext)
    {
        _awpaAcademicDbContext = awpaAcademicDbContext;
    }

    public List<User> GetAll()
    {
        return _awpaAcademicDbContext.Users.ToList();
    }

    public User? GetById(int id)
    {
        return _awpaAcademicDbContext.Users.FirstOrDefault(u => u.Id == id);
    }

    public void DeleteUser(User user)
    {
        _awpaAcademicDbContext.Remove(user);
    }

    public bool EditUser(int Id, User user)
    {
        User? existingEntity = _awpaAcademicDbContext.Users.Find(Id);
        if (existingEntity == null)
        {
            return false;
        }
        else
        {
            _awpaAcademicDbContext.Entry(existingEntity).State = EntityState.Detached;
        }
        _awpaAcademicDbContext.Attach(user);
        _awpaAcademicDbContext.Entry(user).State = EntityState.Modified;
        return true;
    }

    public User AddUser(User user)
    {
        EntityEntry<User> x = _awpaAcademicDbContext.Add(user);
        return x.Entity;
    }
}
