using AwpaAcademic.Api.Exceptions;
using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;
using AwpaAcademic.Api.Repositories.Contracts;
using AwpaAcademic.Api.Services.Contracts;

namespace AwpaAcademic.Api.Services;

public class FacultadService : IFacultadService
{
    public readonly IFacultadRepository _facultadRepository;
    public readonly IUserRepository _userRepository;
    public readonly IFacultadMapper _facultadMapper;

    public FacultadService(
        IFacultadRepository facultadRepository,
        IUserRepository userRepository,
        IFacultadMapper facultadMapper
    )
    {
        _facultadRepository = facultadRepository;
        _userRepository = userRepository;
        _facultadMapper = facultadMapper;
    }

    public async Task<List<FacultadDto>> GetAllAsync()
    {
        List<Facultad> facultadEntity = await _facultadRepository.GetAllAsync();
        List<FacultadDto> facultades = facultadEntity.Select(f => _facultadMapper.MapToFacultadDto(f)).ToList();
        return facultades;
    }

    public async Task<FacultadDto?> GetByIdAsync(string codigoFacultad)
    {
        Facultad? facultad = await _facultadRepository.GetByIdAsync(codigoFacultad);
        if (facultad == null)
        {
            throw new NotFoundException($"No se encontró ninguna facultad con el código {codigoFacultad}.");
        }

        FacultadDto facultadDto = _facultadMapper.MapToFacultadDto(facultad);
        return facultadDto;
    }

    public async Task<Facultad> AddFacultadAsync(FacultadDto facultadDto)
    {
        if (await _facultadRepository.ExistsAsync(facultadDto.CodigoFacultad))
        {
            throw new InvalidOperationException(
                $"Facultad with codigoFacultad {facultadDto.CodigoFacultad} already exists.");
        }

        Facultad facultadEntity = _facultadMapper.MapToFacultad(facultadDto);

        await _facultadRepository.AddFacultadAsync(facultadEntity);
        await SaveChangesAsync();
        return facultadEntity;
    }

    public async Task<bool> EditFacultadAsync(string codigoFacultad, FacultadDto facultadDto)
    {
        if (!await _facultadRepository.ExistsAsync(codigoFacultad))
        {
            throw new NotFoundException($"No se encontró ninguna facultad con el código {codigoFacultad}.");
        }

        if (codigoFacultad != facultadDto.CodigoFacultad)
        {
            throw new InvalidOperationException(
                "El campo 'codigoFacultad' es inmutable y no puede ser modificado después de crear la facultad. Actualiza los demás campos.");
        }

        Facultad facultadEntity = _facultadMapper.MapToFacultad(facultadDto);

        bool result = await _facultadRepository.EditFacultadAsync(codigoFacultad, facultadEntity);
        await SaveChangesAsync();

        return result;
    }

    public async Task<bool> DeleteFacultadAsync(string codigoFacultad)
    {
        bool hasAssociatedUsers = await _userRepository.ExistsWithFacultad(codigoFacultad);

        if (hasAssociatedUsers)
        {
            throw new InvalidOperationException(
                $"Facultad {codigoFacultad} cannot be deleted because it has associated users");
        }

        Facultad? facultad = await _facultadRepository.GetByIdAsync(codigoFacultad);
        if (facultad == null)
        {
            throw new NotFoundException($"No se encontró ninguna facultad con el código {codigoFacultad}.");
        }

        await _facultadRepository.DeleteFacultadAsync(facultad);
        await SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _facultadRepository.SaveChangesAsync();
    }
}
