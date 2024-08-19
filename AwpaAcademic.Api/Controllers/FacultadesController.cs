using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;
using AwpaAcademic.Api.Services.Contracts;
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
        return facultad == null ? NotFound() : Ok(facultad);
    }

    [HttpPost]
    public async Task<IActionResult> AddFacultad([FromBody] CreateFacultadDto createFacultadDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Please add Facultad");
        }

        FacultadDto newFacultad = new
        (
            createFacultadDto.CodigoFacultad,
            createFacultadDto.NombreFacultad
        );


        Facultad addFacultad =
        await _facultadService.AddFacultadAsync(newFacultad);
        FacultadDto facultadDtoResult = _facultadMapper.MapToFacultadDto(addFacultad);
        return Ok(facultadDtoResult);
    }

    [HttpPut("{codigoFacultad}")]
    public async Task<IActionResult> EditFacultad([FromRoute] string codigoFacultad, [FromBody] UpdateFacultadDto updateFacultadDto)
    {
        FacultadDto? existingFacultad = await _facultadService.GetByIdAsync(codigoFacultad);
        if (existingFacultad == null)
        {
            return NotFound("Facultad Not Found!!!!!");
        }

        FacultadDto updateFacultad = new
        (
            CodigoFacultad: codigoFacultad,
            updateFacultadDto.NombreFacultad
        );

        await _facultadService.EditFacultadAsync(codigoFacultad, updateFacultad);
        return Ok(updateFacultad);
    }

    [HttpDelete("{codigoFacultad}")]
    public async Task<IActionResult> DeleteFacultad([FromRoute] string codigoFacultad)
    {
        bool isDeleted = await _facultadService.DeleteFacultadAsync(codigoFacultad);
        if (!isDeleted)
        {
            return NotFound("Facultad Not Fuond!!!!!");
        }

        await _facultadService.SaveChangesAsync();
        return Ok("Facultad Deleted Sucessfuly");
    }
}
