using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;
using AwpaAcademic.Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AwpaAcademic.Api.Controllers;

[ApiController]
[Route("Publicaciones")]
public class PublicacionesController : ControllerBase
{
    private readonly IPublicacionService _publicacionService;
    private readonly IPublicacionMapper _publicacionMapper;

    public PublicacionesController(IPublicacionService publicacionService, IPublicacionMapper publicacionMapper)
    {
        _publicacionService = publicacionService;
        _publicacionMapper = publicacionMapper;
    }

    [HttpGet]
    public async Task<ActionResult> GetPublicaciones()
    {
        ResultDto<List<PublicacionesDto>> result = new ResultDto<List<PublicacionesDto>>();
        result.Results = await _publicacionService.GetAllAsync();
        result.StatusCode = Ok().StatusCode;
        return Ok(result);
    }

    [HttpGet("{idPublicacion}")]
    public async Task<ActionResult> GetPublicacionById([FromRoute] Guid idPublicacion)
    {
        PublicacionDto? publicacion = await _publicacionService.GetByIdAsync(idPublicacion);
        return Ok(publicacion);
    }

    [HttpPost]
    public async Task<ActionResult> AddPublicacion([FromBody] CreatePublicacionDto createPublicacionDto)
    {
        Publicacion publicacion = await _publicacionService.AddPublicacionAsync(createPublicacionDto);
        PublicacionDto result = _publicacionMapper.MapToPublicacionDto(publicacion);
        return Ok(result);
    }

    [HttpPut("{idPublicacion}")]
    public async Task<ActionResult> EditPublicacion([FromRoute] Guid idPublicacion,
        [FromBody] UpdatePublicacionDto updatePublicacionDto)
    {
        await _publicacionService.EditPublicacionAsync(idPublicacion, updatePublicacionDto);
        return Ok();
    }

    [HttpDelete("{idPublicacion}")]
    public async Task<ActionResult> DeletePublicacion([FromRoute] Guid idPublicacion)
    {
        await _publicacionService.DeletePublicacionAsync(idPublicacion);
        return Ok();
    }
}
