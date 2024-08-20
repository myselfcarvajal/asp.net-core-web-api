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
        List<PublicacionesDto> publicaciones = publicacionEntity.Select(p => _publicacionMapper.MapToPublicacionesDto(p)).ToList();
        return publicaciones;
    }

    public async Task<PublicacionDto?> GetByIdAsync(Guid idPublicacion)
    {
        Publicacion? publicacion = await _publicacionRepository.GetByIdAsync(idPublicacion);
        if (publicacion == null)
        {
            return null;
        }
        PublicacionDto publicacionDto = _publicacionMapper.MapToPublicacionDto(publicacion);
        return publicacionDto;
    }

    public async Task<Publicacion> AddPublicacionAsync(PublicacionDto publicacionDto)
    {
        if (!await _userRepository.ExistsAsync(publicacionDto.UserId))
        {
            throw new InvalidOperationException("UserId doesn't exist.");
        }

        if (await _facultadRepository.GetByIdAsync(publicacionDto.CodigoFacultad) == null)
        {
            throw new InvalidOperationException("Codigofacultad doesn't exist.");
        }

        Publicacion publicacionEntity = _publicacionMapper.MapToPublicacion(publicacionDto);

        await _publicacionRepository.AddPublicacionAsync(publicacionEntity);
        await SaveChangesAsync();
        return publicacionEntity;
    }

    public async Task<bool> EditPublicacionAsync(Guid idPublicacion, PublicacionDto publicacionDto)
    {
        if (!await _userRepository.ExistsAsync(publicacionDto.UserId))
        {
            throw new InvalidOperationException("UserId doesn't exist.");
        }

        if (await _facultadRepository.GetByIdAsync(publicacionDto.CodigoFacultad) == null)
        {
            throw new InvalidOperationException("Codigofacultad doesn't exist.");
        }

        Publicacion publicacionEntity = _publicacionMapper.MapToPublicacion(publicacionDto);

        bool result = await _publicacionRepository.EditPublicacionAsync(idPublicacion, publicacionEntity);
        await SaveChangesAsync();
        return result;
    }

    public async Task<bool> DeletePublicacionAsync(Guid idPublicacion)
    {
        Publicacion? publicacion = await _publicacionRepository.GetByIdAsync(idPublicacion);
        if (publicacion == null)
        {
            return false;
        }
        await _publicacionRepository.DeletePublicacionAsync(publicacion);
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _publicacionRepository.SaveChangesAsync();
    }
}
