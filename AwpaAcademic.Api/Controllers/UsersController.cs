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
    public ActionResult GetUsers()
    {
        ResultDto<List<UserDto>> result = new ResultDto<List<UserDto>>();
        result.Results = _userService.GetAll();
        result.StatusCode = Ok().StatusCode;
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult GetUserById([FromRoute] int id)
    {
        UserDto? user = _userService.GetById(id);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public ActionResult AddUser([FromBody] CreateUserDto createUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Please add User");
        }
        else
        {
            try
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
                User addUser = _userService.AddUser(newUser);
                _userService.SaveChanges();
                UserDto userDtoResult = _userMapper.MapToUserDto(addUser);
                return Ok(userDtoResult);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }

    [HttpPut("{id}")]
    public ActionResult EditUser([FromRoute] int id, [FromBody] UpdateUserDto updateUserDto)
    {

        UserDto? existingUser = _userService.GetById(id);

        if (existingUser == null){
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

        _userService.EditUser(id, updateUser);
        _userService.SaveChanges();
        return Ok(updateUser);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUser([FromRoute] int id)
    {
        bool isDeleted = _userService.DeleteUser(id);
        if (!isDeleted)
        {
            return NotFound("User Not Fuond!!!!!");
        }
        _userService.SaveChanges();
        return Ok("User Deleted Sucessfuly");
    }

}
