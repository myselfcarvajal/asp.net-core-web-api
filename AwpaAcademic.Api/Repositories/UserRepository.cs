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
        return await _awpaAcademicDbContext.Users
        .Include(u => u.Facultad)
        .ToListAsync();
    }

    public Task<bool> ExistsAsync(int id)
    {
        return _awpaAcademicDbContext.Users.AnyAsync(u => u.Id == id);
    }

    public async Task<bool> ExistsWithFacultad(string codigoFacultad)
    {
        return await _awpaAcademicDbContext.Users.AnyAsync(u => u.Codigofacultad == codigoFacultad);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _awpaAcademicDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _awpaAcademicDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<List<Publicacion>> GetPublicacionesByUserIdAsync(int id)
    {
        return await _awpaAcademicDbContext.Publicaciones
        .Where(p => p.UserId == id)
        .Include(u => u.Facultad)
        .Include(u => u.User)

        // .Select(p => (Publicacion?)p)
        .ToListAsync();
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
