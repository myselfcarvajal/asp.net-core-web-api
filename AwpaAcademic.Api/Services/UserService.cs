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

    public async Task<List<UserDto>> GetAllAsync()
    {
        List<User> _users = await _userRepository.GetAllAsync();
        List<UserDto> users = _users.Select(u => _userMapper.MapToUserDto(u)).ToList();
        return users;
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        User? user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return null;
        }
        UserDto userDto = _userMapper.MapToUserDto(user);
        return userDto;
    }

    public async Task<User> AddUserAsync(UserDto userDto)
    {
        User userEntity = _userMapper.MapToUser(userDto);
        return await _userRepository.AddUserAsync(userEntity);
    }

    public async Task<bool> EditUserAsync(int id, UserDto userDto)
    {
        User userEntity = _userMapper.MapToUser(userDto);
        return await _userRepository.EditUserAsync(id, userEntity);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        User? user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return false;
        }
        await _userRepository.DeleteUserAsync(user);
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _userRepository.SaveChangesAsync();
    }
}
