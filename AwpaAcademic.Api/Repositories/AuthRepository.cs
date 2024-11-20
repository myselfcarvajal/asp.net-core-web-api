using AwpaAcademic.Api.Data;
using AwpaAcademic.Api.Models.Entities;
using AwpaAcademic.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AwpaAcademic.Api.Repositories;

public class AuthRepository : BaseRepository, IAuthRepository
{
    private AwpaAcademicDbContext _awpaAcademicDbContext;

    public AuthRepository(AwpaAcademicDbContext awpaAcademicDbContext)
        : base(awpaAcademicDbContext)
    {
        _awpaAcademicDbContext = awpaAcademicDbContext;
    }

    public async Task<User> RegisterUserAsync(User user)
    {
        EntityEntry<User> x = await _awpaAcademicDbContext.AddAsync(user);
        return x.Entity;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _awpaAcademicDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
