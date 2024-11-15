using AwpaAcademic.Api.Data;
using AwpaAcademic.Api.Models.Entities;
using AwpaAcademic.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AwpaAcademic.Api.Repositories;

public class FacultadRepository : BaseRepository, IFacultadRepository
{
    private AwpaAcademicDbContext _awpaAcademicDbContext;

    public FacultadRepository(AwpaAcademicDbContext awpaAcademicDbContext)
        : base(awpaAcademicDbContext)
    {
        _awpaAcademicDbContext = awpaAcademicDbContext;
    }

    public async Task<List<Facultad>> GetAllAsync()
    {
        return await _awpaAcademicDbContext.Facultades.ToListAsync();
    }

    public async Task<Facultad?> GetByIdAsync(string codigoFacultad)
    {
        return await _awpaAcademicDbContext.Facultades
            .FirstOrDefaultAsync(f => f.CodigoFacultad == codigoFacultad);
    }

    public async Task<bool> ExistsAsync(string codigoFacultad)
    {
        return await _awpaAcademicDbContext.Facultades.AnyAsync(f => f.CodigoFacultad == codigoFacultad);
    }

    public async Task<Facultad> AddFacultadAsync(Facultad facultad)
    {
        EntityEntry<Facultad> x = await _awpaAcademicDbContext.AddAsync(facultad);
        return x.Entity;
    }

    public async Task<bool> EditFacultadAsync(string codigoFacultad, Facultad facultad)
    {
        Facultad? existingEntity = await _awpaAcademicDbContext.Facultades.FindAsync(codigoFacultad);
        if (existingEntity == null)
        {
            return false;
        }
        else
        {
            _awpaAcademicDbContext.Entry(existingEntity).State = EntityState.Detached;
        }

        _awpaAcademicDbContext.Attach(facultad);
        _awpaAcademicDbContext.Entry(facultad).State = EntityState.Modified;
        return true;
    }

    public async Task DeleteFacultadAsync(Facultad facultad)
    {
        await Task.Run(() => _awpaAcademicDbContext.Remove(facultad));
    }
}
