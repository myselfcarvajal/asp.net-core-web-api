using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Mappers.Contracts;

public interface IFacultadMapper
{
    public Facultad MapToFacultad(FacultadDto facultadDto);
    public FacultadDto MapToFacultadDto(Facultad facultad);
}
