using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Repositories.Contracts;

public interface IFacultadRepository
{
    Task<List<Facultad>> GetAllAsync();
    Task<Facultad?> GetByIdAsync(string codigoFacultad);
    Task<Facultad> AddFacultadAsync(Facultad facultad);
    Task DeleteFacultadAsync(Facultad facultad);
    Task<bool> EditFacultadAsync(string codigoFacultad, Facultad facultad);
}