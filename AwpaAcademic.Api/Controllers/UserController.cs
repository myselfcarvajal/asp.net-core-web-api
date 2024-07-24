using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;
using AwpaAcademic.Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AwpaAcademic.Api.Controllers;

[ApiController]
[Route("User")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserMapper _userMapper;

    public UserController(IUserService userService, IUserMapper userMapper)
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
    public ActionResult AddUser([FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Please add User");
        }
        else
        {
            try
            {
                User AddUser = _userService.AddUser(userDto);
                _userService.SaveChanges();
                UserDto userDtoResult = _userMapper.MapToUserDto(AddUser);
                return Ok(userDtoResult);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }

    [HttpPut("{id}")]
    public ActionResult EditUser([FromRoute] int id, [FromBody] UserDto userDto)
    {
        bool isEdited = _userService.EditUser(id, userDto);
        if (!isEdited)
        {
            return NotFound("User Not Fuond!!!!!");
        }

        _userService.SaveChanges();
        return Ok(userDto);
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
