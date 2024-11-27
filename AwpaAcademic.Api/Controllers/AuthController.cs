using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AwpaAcademic.Api.Controllers;

[ApiController]
[Route("Auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    private readonly IUserMapper _userMapper;


    public AuthController(IUserService userService,
        IUserMapper userMapper,
        IAuthService authService)
    {
        _userService = userService;
        _userMapper = userMapper;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        var user = await _authService.RegisterUserAsync(registerUserDto);
        var result = _userMapper.MapToUserDto(user);
        return Ok(result);
    }

    [HttpPost("signin")]
    public async Task<ActionResult> Signin([FromBody] SigninDto signinDto)
    {
        var token = await _authService.Singin(signinDto);
        return Ok(new { Token = token });
    }
}
