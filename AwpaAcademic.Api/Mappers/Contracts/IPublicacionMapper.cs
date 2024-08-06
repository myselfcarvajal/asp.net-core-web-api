using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Mappers.Contracts;

public interface IPublicacionMapper
{
    public Publicacion MapToPublicacion(PublicacionDto publicacionDto);
    public PublicacionDto MapToPublicacionDto(Publicacion publicacion);
    public PublicacionesDto MapToPublicacionesDto(Publicacion publicacion);
}
