using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;
using AwpaAcademic.Api.Repositories.Contracts;
using AwpaAcademic.Api.Services.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AwpaAcademic.Api.Services;

public class UserService : IUserService
{
    public readonly IUserRepository _userRepository;
    public readonly IUserMapper _userMapper;
    public readonly IPublicacionMapper _publicacionMapper;
    public UserService(IUserRepository userRepository, IUserMapper userMapper, IPublicacionMapper publicacionMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
        _publicacionMapper = publicacionMapper;
    }

    public async Task<List<UsersDto>> GetAllAsync()
    {
        List<User> userEntity = await _userRepository.GetAllAsync();
        List<UsersDto> users = userEntity.Select(u => _userMapper.MapToUsersDto(u)).ToList();
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

    public async Task<List<PublicacionesDto>> GetPublicacionesByUserIdAsync(int id)
    {
        List<Publicacion> publicacionEntity = await _userRepository.GetPublicacionesByUserIdAsync(id);
        List<PublicacionesDto> publicaciones = publicacionEntity.Select(p => _publicacionMapper.MapToPublicacionesDto(p)).ToList();
        return publicaciones;
    }

    public async Task<User> AddUserAsync(UserDto userDto)
    {
        try
        {
            User userEntity = _userMapper.MapToUser(userDto);

            await _userRepository.AddUserAsync(userEntity);
            await SaveChangesAsync();
            return userEntity;
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            if (sqlEx.Number == 2601 || sqlEx.Number == 2627)
            {
                if (sqlEx.Message.Contains("PK_Users"))
                {
                    throw new InvalidOperationException("User id already exists.", ex);
                }
                else if (sqlEx.Message.Contains("IX_Users_Email"))
                {
                    throw new InvalidOperationException("Email address already exists.", ex);
                }
            }
            throw;
        }
    }

    public async Task<bool> EditUserAsync(int id, UserDto userDto)
    {
        try
        {
            User userEntity = _userMapper.MapToUser(userDto);

            bool result = await _userRepository.EditUserAsync(id, userEntity);
            await SaveChangesAsync();
            return result;
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            if (sqlEx.Number == 2601 || sqlEx.Number == 2627)
            {
                if (sqlEx.Message.Contains("PK_Users"))
                {
                    throw new InvalidOperationException("User id already exists.", ex);
                }
                else if (sqlEx.Message.Contains("IX_Users_Email"))
                {
                    throw new InvalidOperationException("Email address already exists.", ex);
                }
            }
            throw;
        }
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
