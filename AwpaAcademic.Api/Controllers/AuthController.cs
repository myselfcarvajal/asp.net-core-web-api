using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AwpaAcademic.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserMapper _userMapper;


    public AuthController(IUserService userService, IUserMapper userMapper)
    {
        _userService = userService;
        _userMapper = userMapper;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] CreateUserDto createUserDto)
    {
        var user = await _userService.AddUserAsync(createUserDto);
        var result = _userMapper.MapToUserDto(user);
        return Ok(result);
    }
}
