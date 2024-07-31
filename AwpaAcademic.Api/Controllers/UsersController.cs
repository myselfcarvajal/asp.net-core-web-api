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
        ResultDto<List<UserDto>> result = new ResultDto<List<UserDto>>();
        result.Results = await _userService.GetAllAsync();
        result.StatusCode = Ok().StatusCode;
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetUserById([FromRoute] int id)
    {
        UserDto? user = await _userService.GetByIdAsync(id);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> AddUser([FromBody] CreateUserDto createUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Please add User");
        }
        else
        {
            // Convertir CreateUserDto a UserDto
            UserDto newUser = new
            (
                createUserDto.Id,
                createUserDto.Email,
                createUserDto.Passwd,
                createUserDto.Nombre,
                createUserDto.Apellido,
                createUserDto.RoleId,
                createUserDto.Codigofacultad,
                DateTime.Now,
                DateTime.Now
            );

            User addUser = await _userService.AddUserAsync(newUser);
            UserDto userDtoResult = _userMapper.MapToUserDto(addUser);
            return Ok(userDtoResult);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> EditUser([FromRoute] int id, [FromBody] UpdateUserDto updateUserDto)
    {

        UserDto? existingUser = await _userService.GetByIdAsync(id);

        if (existingUser == null)
        {
            return NotFound("User Not Found!!!!!");
        }

        UserDto updateUser = new
        (
            updateUserDto.Id,
            updateUserDto.Email,
            updateUserDto.Passwd,
            updateUserDto.Nombre,
            updateUserDto.Apellido,
            updateUserDto.RoleId,
            updateUserDto.Codigofacultad,
            CreatedAd: existingUser.CreatedAd,
            UpdatedAt: DateTime.Now
        );

        await _userService.EditUserAsync(id, updateUser);
        return Ok(updateUser);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        bool isDeleted = await _userService.DeleteUserAsync(id);
        if (!isDeleted)
        {
            return NotFound("User Not Fuond!!!!!");
        }

        await _userService.SaveChangesAsync();
        return Ok("User Deleted Sucessfuly");
    }

}
