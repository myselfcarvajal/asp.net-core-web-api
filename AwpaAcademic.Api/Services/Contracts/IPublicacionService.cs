using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Services.Contracts;

public interface IPublicacionService
{
    Task<List<PublicacionesDto>> GetAllAsync();
    Task<PublicacionDto?> GetByIdAsync(Guid idPublicacion);
    Task<Publicacion> AddPublicacionAsync(CreatePublicacionDto createPublicacionDto);
    Task<bool> DeletePublicacionAsync(Guid idPublicacion);
    Task<bool> EditPublicacionAsync(Guid idPublicacion, UpdatePublicacionDto updatePublicacionDto);
    Task SaveChangesAsync();
}
