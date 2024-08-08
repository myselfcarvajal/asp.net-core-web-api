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
        return publicacion == null ? NotFound() : Ok(publicacion);
    }

    [HttpPost]
    public async Task<ActionResult> AddPublicacion([FromBody] CreatePublicacionDto createPublicacionDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Please add Publicacion");
        }
        else
        {
            // Convertir CreatePublicacionDto a PublicacionDto
            PublicacionDto newPublicacion = new
            (
                IdPublicacion: Guid.NewGuid(),
                createPublicacionDto.Titulo,
                createPublicacionDto.Autor,
                createPublicacionDto.Descripcion,
                createPublicacionDto.Url,
                createPublicacionDto.UserId,
                createPublicacionDto.CodigoFacultad,
                DateTime.Now,
                DateTime.Now
            );

            Publicacion addPublicacion = await _publicacionService.AddPublicacionAsync(newPublicacion);
            PublicacionDto publicacionResult = _publicacionMapper.MapToPublicacionDto(addPublicacion);
            return Ok(publicacionResult);
        }
    }

    [HttpPut("{idPublicacion}")]
    public async Task<ActionResult> EditPublicacion([FromRoute] Guid idPublicacion, [FromBody] UpdatePublicacionDto updatePublicacionDto)
    {
        PublicacionDto? existingPublicacion = await _publicacionService.GetByIdAsync(idPublicacion);

        if (existingPublicacion == null)
        {
            return NotFound("Publicacion Not Found!!!!!");
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
            CreatedAt: existingPublicacion.CreatedAt,
            UpdatedAt: DateTime.Now
        );

        await _publicacionService.EditPublicacionAsync(idPublicacion, updatePublicacion);
        return Ok(updatePublicacion);

    }

    [HttpDelete("{idPublicacion}")]
    public async Task<ActionResult> DeletePublicacion([FromRoute] Guid idPublicacion)
    {
        bool isDeleted = await _publicacionService.DeletePublicacionAsync(idPublicacion);
        if (!isDeleted)
        {
            return NotFound("Publicacion Not Fuond!!!!!");
        }

        await _publicacionService.SaveChangesAsync();
        return Ok("Publicacion Deleted Sucessfuly");
    }
}
