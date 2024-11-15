using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Repositories.Contracts;

public interface IPublicacionRepository
{
    Task<List<Publicacion>> GetAllAsync();
    Task<bool> ExistsAsync(Guid idPublicacion);
    Task<Publicacion?> GetByIdAsync(Guid idPublicacion);
    Task<Publicacion> AddPublicacionAsync(Publicacion publicacion);
    Task DeletePublicacionAsync(Publicacion publicacion);
    Task<bool> EditPublicacionAsync(Guid idPublicacion, Publicacion publicacion);
    Task SaveChangesAsync();
}
