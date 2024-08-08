using AwpaAcademic.Api.Data;
using AwpaAcademic.Api.Models.Entities;
using AwpaAcademic.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AwpaAcademic.Api.Repositories;

public class PublicacionRepository : BaseRepository, IPublicacionRepository
{
    private AwpaAcademicDbContext _awpaAcademicDbContext;
    public PublicacionRepository(AwpaAcademicDbContext awpaAcademicDbContext) : base(awpaAcademicDbContext)
    {
        _awpaAcademicDbContext = awpaAcademicDbContext;
    }

    public async Task<List<Publicacion>> GetAllAsync()
    {
        return await _awpaAcademicDbContext.Publicaciones
        .Include(p => p.User)
        .Include(p => p.Facultad)
        .ToListAsync();
    }

    public async Task<Publicacion?> GetByIdAsync(Guid idPublicacion)
    {
        return await _awpaAcademicDbContext.Publicaciones
        .FirstOrDefaultAsync(p => p.IdPublicacion == idPublicacion);
    }

    public async Task DeletePublicacionAsync(Publicacion publicacion)
    {
        await Task.Run(() => _awpaAcademicDbContext.Remove(publicacion));
    }

    public async Task<bool> EditPublicacionAsync(Guid idPublicacion, Publicacion publicacion)
    {
        Publicacion? existingEntity = await _awpaAcademicDbContext.Publicaciones.FindAsync(idPublicacion);
        if (existingEntity == null)
        {
            return false;
        }
        else
        {
            _awpaAcademicDbContext.Entry(existingEntity).State = EntityState.Detached;
        }

        _awpaAcademicDbContext.Attach(publicacion);
        _awpaAcademicDbContext.Entry(publicacion).State = EntityState.Modified;
        return true;
    }

    public async Task<Publicacion> AddPublicacionAsync(Publicacion publicacion)
    {
        EntityEntry<Publicacion> x = await _awpaAcademicDbContext.AddAsync(publicacion);
        return x.Entity;
    }
}
