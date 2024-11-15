using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;
using AwpaAcademic.Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AwpaAcademic.Api.Controllers;

[ApiController]
[Route("Users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserMapper _userMapper;

    public UsersController(IUserService userService, IUserMapper userMapper)
    {
        _userService = userService;
        _userMapper = userMapper;
    }

    [HttpGet]
    public async Task<ActionResult> GetUsers()
    {
        ResultDto<List<UsersDto>> result = new ResultDto<List<UsersDto>>();
        result.Results = await _userService.GetAllAsync();
        result.StatusCode = Ok().StatusCode;
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetUserById([FromRoute] int id)
    {
        UserDto? user = await _userService.GetByIdAsync(id);
        return Ok(user);
    }

    [HttpGet("{id}/publicaciones")]
    public async Task<ActionResult> GetPublicacionesByUserId([FromRoute] int id)
    {
        List<PublicacionesDto> publicaciones = await _userService.GetPublicacionesByUserIdAsync(id);
        return Ok(publicaciones);
    }

    [HttpPost]
    public async Task<ActionResult> AddUser([FromBody] CreateUserDto createUserDto)
    {
        User addUser = await _userService.AddUserAsync(createUserDto);
        UserDto result = _userMapper.MapToUserDto(addUser);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> EditUser([FromRoute] int id, [FromBody] UpdateUserDto updateUserDto)
    {
        await _userService.EditUserAsync(id, updateUserDto);
        return Ok(updateUserDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        await _userService.DeleteUserAsync(id);
        return Ok();
    }
}
