using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Services.Contracts;

public interface IFacultadService
{
    Task<List<FacultadDto>> GetAllAsync();
    Task<FacultadDto?> GetByIdAsync(string codigoFacultad);
    Task<Facultad> AddFacultadAsync(FacultadDto facultadDto);
    Task<bool> DeleteFacultadAsync(string codigoFacultad);
    Task<bool> EditFacultadAsync(string codigoFacultad, FacultadDto facultadDto);
    Task SaveChangesAsync();
}
