using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;
using AwpaAcademic.Api.Repositories.Contracts;
using AwpaAcademic.Api.Services.Contracts;

namespace AwpaAcademic.Api.Services;

public class UserService : IUserService
{
    public readonly IUserRepository _userRepository;
    public readonly IUserMapper _userMapper;
    public UserService(IUserRepository userRepository, IUserMapper userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }

    public List<UserDto> GetAll()
    {
        List<UserDto> users = new List<UserDto>();
        users = _userRepository.GetAll().Select(u => _userMapper.MapToUserDto(u)).ToList();
        return users;
    }

    public UserDto? GetById(int id)
    {
        User? user = _userRepository.GetById(id);
        if (user == null)
        {
            return null;
        }
        UserDto userDto = _userMapper.MapToUserDto(user);
        return userDto;
    }

    public User AddUser(UserDto userDto)
    {
        User userEntity = _userMapper.MapToUser(userDto);
        return _userRepository.AddUser(userEntity);
    }

    public bool EditUser(int id, UserDto userDto)
    {
        User userEntity = _userMapper.MapToUser(userDto);
        return _userRepository.EditUser(id, userEntity);
    }

    public bool DeleteUser(int id)
    {
        User? user = _userRepository.GetById(id);
        if (user == null)
        {
            return false;
        }
        _userRepository.DeleteUser(user);
        return true;
    }

    public void SaveChanges()
    {
        _userRepository.SaveChanges();
    }
}
