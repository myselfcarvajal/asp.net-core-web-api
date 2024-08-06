using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Mappers;

public class PublicacionMapper : IPublicacionMapper
{
    public Publicacion MapToPublicacion(PublicacionDto publicacionDto)
    {
        return new Publicacion()
        {
            Titulo = publicacionDto.Titulo,
            Autor = publicacionDto.Autor,
            Descripcion = publicacionDto.Descripcion,
            Url = publicacionDto.Url,
            UserId = publicacionDto.UserId,
            CodigoFacultad = publicacionDto.CodigoFacultad,
            CreatedAt = publicacionDto.CreatedAt,
            UpdatedAt = publicacionDto.UpdatedAt,

        };
    }

    public PublicacionDto MapToPublicacionDto(Publicacion publicacion)
    {
        return new(
            publicacion.Titulo,
            publicacion.Autor,
            publicacion.Descripcion,
            publicacion.Url ?? string.Empty,
            publicacion.UserId,
            publicacion.CodigoFacultad,
            publicacion.CreatedAt,
            publicacion.UpdatedAt
        );
    }

    public PublicacionesDto MapToPublicacionesDto(Publicacion publicacion)
    {
        return new(
            publicacion.IdPublicacion,
            publicacion.Titulo,
            publicacion.Autor,
            publicacion.Descripcion,
            publicacion.Url ?? string.Empty,
            publicacion.User == null ? null : new UserDetailsInPublicacionDto(
                publicacion.User.Id,
                publicacion.User.Email,
                publicacion.User.Nombre,
                publicacion.User.Apellido,
                publicacion.User.Codigofacultad ?? string.Empty
            ),
            publicacion.Facultad == null ? null : new FacultadDto(
                publicacion.Facultad.CodigoFacultad,
                publicacion.Facultad.NombreFacultad
            ),
            publicacion.CreatedAt,
            publicacion.UpdatedAt
        );
    }
}
