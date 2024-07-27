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

    public async Task<List<User>> GetAllAsync()
    {
        return await _awpaAcademicDbContext.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _awpaAcademicDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task DeleteUserAsync(User user)
    {
        await Task.Run(() => _awpaAcademicDbContext.Remove(user));

    }

    public async Task<bool> EditUserAsync(int Id, User user)
    {
        User? existingEntity = await _awpaAcademicDbContext.Users.FindAsync(Id);
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

    public async Task<User> AddUserAsync(User user)
    {
        EntityEntry<User> x = await _awpaAcademicDbContext.AddAsync(user);
        return x.Entity;
    }
}
