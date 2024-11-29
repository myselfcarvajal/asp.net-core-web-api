using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;
using AwpaAcademic.Api.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AwpaAcademic.Api.Controllers;

[ApiController]
[Route("Facultades")]
public class FacultadesController : ControllerBase
{
    private readonly IFacultadService _facultadService;
    private readonly IFacultadMapper _facultadMapper;

    public FacultadesController(
        IFacultadService facultadService,
        IFacultadMapper facultadMapper)
    {
        _facultadService = facultadService;
        _facultadMapper = facultadMapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetFacultades()
    {
        ResultDto<List<FacultadDto>> result = new ResultDto<List<FacultadDto>>();
        result.Results = await _facultadService.GetAllAsync();
        result.StatusCode = Ok().StatusCode;
        return Ok(result);
    }

    [HttpGet("{codigoFacultad}")]
    public async Task<IActionResult> GetFacultadById([FromRoute] string codigoFacultad)
    {
        FacultadDto? facultad = await _facultadService.GetByIdAsync(codigoFacultad);
        return Ok(facultad);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddFacultad([FromBody] FacultadDto facultadDto)
    {
        Facultad facultad =
            await _facultadService.AddFacultadAsync(facultadDto);
        FacultadDto result = _facultadMapper.MapToFacultadDto(facultad);
        return Ok(result);
    }

    [HttpPut("{codigoFacultad}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> EditFacultad([FromRoute] string codigoFacultad,
        [FromBody] FacultadDto facultadDto)
    {
        await _facultadService.EditFacultadAsync(codigoFacultad, facultadDto);
        return Ok(facultadDto);
    }

    [HttpDelete("{codigoFacultad}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteFacultad([FromRoute] string codigoFacultad)
    {
        await _facultadService.DeleteFacultadAsync(codigoFacultad);
        return Ok();
    }
}
