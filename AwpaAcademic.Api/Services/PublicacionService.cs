using AwpaAcademic.Api.Exceptions;
using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;
using AwpaAcademic.Api.Repositories.Contracts;
using AwpaAcademic.Api.Services.Contracts;

namespace AwpaAcademic.Api.Services;

public class PublicacionService : IPublicacionService
{
    public readonly IPublicacionRepository _publicacionRepository;
    public readonly IUserRepository _userRepository;
    public readonly IFacultadRepository _facultadRepository;
    public readonly IPublicacionMapper _publicacionMapper;

    public PublicacionService(
        IPublicacionRepository publicacionRepository,
        IUserRepository userRepository,
        IFacultadRepository facultadRepository,
        IPublicacionMapper publicacionMapper)
    {
        _publicacionRepository = publicacionRepository;
        _userRepository = userRepository;
        _facultadRepository = facultadRepository;
        _publicacionMapper = publicacionMapper;
    }

    public async Task<List<PublicacionesDto>> GetAllAsync()
    {
        List<Publicacion> publicacionEntity = await _publicacionRepository.GetAllAsync();
        List<PublicacionesDto> publicaciones =
            publicacionEntity.Select(p => _publicacionMapper.MapToPublicacionesDto(p)).ToList();
        return publicaciones;
    }

    public async Task<PublicacionDto?> GetByIdAsync(Guid idPublicacion)
    {
        Publicacion? publicacion = await _publicacionRepository.GetByIdAsync(idPublicacion);
        if (publicacion == null)
        {
            throw new NotFoundException($"No se encontró ninguna publiacion con el id {idPublicacion}.");
        }

        PublicacionDto publicacionDto = _publicacionMapper.MapToPublicacionDto(publicacion);
        return publicacionDto;
    }

    public async Task<Publicacion> AddPublicacionAsync(CreatePublicacionDto createPublicacionDto)
    {
        if (!await _userRepository.ExistsAsync(createPublicacionDto.UserId))
        {
            throw new InvalidOperationException($"UserId {createPublicacionDto.UserId} doesn't exist.");
        }

        if (await _facultadRepository.GetByIdAsync(createPublicacionDto.CodigoFacultad) == null)
        {
            throw new InvalidOperationException($"Codigofacultad {createPublicacionDto.CodigoFacultad} doesn't exist.");
        }

        // Convertir CreatePublicacionDto a PublicacionDto
        PublicacionDto newPublicacion = new PublicacionDto
        (
            IdPublicacion: Guid.NewGuid(),
            Titulo: createPublicacionDto.Titulo,
            Autor: createPublicacionDto.Autor,
            Descripcion: createPublicacionDto.Descripcion,
            Url: createPublicacionDto.Url,
            UserId: createPublicacionDto.UserId,
            CodigoFacultad: createPublicacionDto.CodigoFacultad,
            CreatedAt: DateTime.Now,
            UpdatedAt: DateTime.Now
        );

        Publicacion publicacionEntity = _publicacionMapper.MapToPublicacion(newPublicacion);

        await _publicacionRepository.AddPublicacionAsync(publicacionEntity);
        await SaveChangesAsync();
        return publicacionEntity;
    }

    public async Task<bool> EditPublicacionAsync(Guid idPublicacion, UpdatePublicacionDto updatePublicacionDto)
    {
        Publicacion? publicacion = await _publicacionRepository.GetByIdAsync(idPublicacion);

        if (publicacion == null)
        {
            throw new NotFoundException($"No se encontró ninguna publicacion con el id {idPublicacion}.");
        }

        if (!await _userRepository.ExistsAsync(updatePublicacionDto.UserId))
        {
            throw new InvalidOperationException("UserId doesn't exist.");
        }

        if (await _facultadRepository.GetByIdAsync(updatePublicacionDto.CodigoFacultad) == null)
        {
            throw new InvalidOperationException("Codigofacultad doesn't exist.");
        }

        PublicacionDto updatePublicacion = new
        (
            IdPublicacion: idPublicacion,
            updatePublicacionDto.Titulo,
            updatePublicacionDto.Autor,
            updatePublicacionDto.Descripcion,
            updatePublicacionDto.Url,
            updatePublicacionDto.UserId,
            updatePublicacionDto.CodigoFacultad,
            CreatedAt: publicacion.CreatedAt,
            UpdatedAt: DateTime.Now
        );

        Publicacion publicacionEntity = _publicacionMapper.MapToPublicacion(updatePublicacion);

        bool result = await _publicacionRepository.EditPublicacionAsync(idPublicacion, publicacionEntity);
        await SaveChangesAsync();
        return result;
    }

    public async Task<bool> DeletePublicacionAsync(Guid idPublicacion)
    {
        Publicacion? publicacion = await _publicacionRepository.GetByIdAsync(idPublicacion);
        if (publicacion == null)
        {
            throw new NotFoundException($"No se encontró ninguna publicacion con el id {idPublicacion}");
        }

        await _publicacionRepository.DeletePublicacionAsync(publicacion);
        await SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _publicacionRepository.SaveChangesAsync();
    }
}
