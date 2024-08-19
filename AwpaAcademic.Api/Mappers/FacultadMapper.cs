using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Mappers;

public class FacultadMapper : IFacultadMapper
{
    public Facultad MapToFacultad(FacultadDto facultadDto)
    {
        return new Facultad()
        {
            CodigoFacultad = facultadDto.CodigoFacultad,
            NombreFacultad = facultadDto.NombreFacultad,
        };
    }

    public FacultadDto MapToFacultadDto(Facultad facultad)
    {
        return new(
            facultad.CodigoFacultad,
            facultad.NombreFacultad
        );
    }
}
