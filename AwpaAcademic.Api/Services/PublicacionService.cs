using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;
using AwpaAcademic.Api.Repositories.Contracts;
using AwpaAcademic.Api.Services.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AwpaAcademic.Api.Services;

public class PublicacionService : IPublicacionService
{
    public readonly IPublicacionRepository _publicacionRepository;
    public readonly IPublicacionMapper _publicacionMapper;

    public PublicacionService(IPublicacionRepository publicacionRepository, IPublicacionMapper publicacionMapper)
    {
        _publicacionRepository = publicacionRepository;
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
        try
        {
            Publicacion publicacionEntity = _publicacionMapper.MapToPublicacion(publicacionDto);

            await _publicacionRepository.AddPublicacionAsync(publicacionEntity);
            await SaveChangesAsync();
            return publicacionEntity;
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            if (sqlEx.Message.Contains("FK_Publicaciones_Users_UserId"))
            {
                throw new InvalidOperationException("The UserId provided does not exist.", ex);
            }
            else if (sqlEx.Message.Contains("FK_Publicaciones_Facultades_CodigoFacultad"))
            {
                throw new InvalidOperationException("The CodigoFacultad provided does not exist.", ex);
            }
            throw;
        }

    }

    public async Task<bool> EditPublicacionAsync(Guid idPublicacion, PublicacionDto publicacionDto)
    {
        try
        {
            Publicacion publicacionEntity = _publicacionMapper.MapToPublicacion(publicacionDto);

            bool result = await _publicacionRepository.EditPublicacionAsync(idPublicacion, publicacionEntity);
            await SaveChangesAsync();
            return result;
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            if (sqlEx.Message.Contains("FK_Publicaciones_Users_UserId"))
            {
                throw new InvalidOperationException("The UserId provided does not exist.", ex);
            }
            else if (sqlEx.Message.Contains("FK_Publicaciones_Facultades_CodigoFacultad"))
            {
                throw new InvalidOperationException("The CodigoFacultad provided does not exist.", ex);
            }
            throw;
        }

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
