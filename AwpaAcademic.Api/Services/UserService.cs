using AwpaAcademic.Api.Exceptions;
using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;
using AwpaAcademic.Api.Repositories.Contracts;
using AwpaAcademic.Api.Services.Contracts;

namespace AwpaAcademic.Api.Services;

public class UserService : IUserService
{
    public readonly IUserRepository _userRepository;
    public readonly IFacultadRepository _facultadRepository;
    public readonly IUserMapper _userMapper;
    public readonly IPublicacionMapper _publicacionMapper;

    public UserService(IUserRepository userRepository,
        IFacultadRepository facultadRepository,
        IUserMapper userMapper,
        IPublicacionMapper publicacionMapper)
    {
        _userRepository = userRepository;
        _facultadRepository = facultadRepository;
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
            throw new NotFoundException($"User with id {id} not found");
        }

        UserDto userDto = _userMapper.MapToUserDto(user);
        return userDto;
    }

    public async Task<List<PublicacionesDto>> GetPublicacionesByUserIdAsync(int id)
    {
        User? user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException($"No se encontró ningun usuario con el id {id}.");
        }

        List<Publicacion> publicacionEntity = await _userRepository.GetPublicacionesByUserIdAsync(id);
        List<PublicacionesDto> publicaciones =
            publicacionEntity.Select(p => _publicacionMapper.MapToPublicacionesDto(p)).ToList();
        return publicaciones;
    }

    public async Task<User> AddUserAsync(CreateUserDto createUserDto)
    {
        if (await _userRepository.ExistsAsync(createUserDto.Id))
        {
            throw new InvalidOperationException("User id already exists.");
        }

        if (await _userRepository.GetByEmailAsync(createUserDto.Email) != null)
        {
            throw new InvalidOperationException("Email already exists.");
        }

        if (await _facultadRepository.GetByIdAsync(createUserDto.Codigofacultad) == null)
        {
            throw new InvalidOperationException("Codigofacultad doesn't exist.");
        }

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

        User userEntity = _userMapper.MapToUser(newUser);

        await _userRepository.AddUserAsync(userEntity);
        await SaveChangesAsync();

        return userEntity;
    }

    public async Task<bool> EditUserAsync(int id, UpdateUserDto updateUserDto)
    {
        User? existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser == null)
        {
            throw new NotFoundException($"No se encontró ningun usuario con el código {id}.");
        }

        // Obtener el usuario con el mismo email
        var existingUserWithEmail = await _userRepository.GetByEmailAsync(updateUserDto.Email);

        // Validar si el email ya está en uso por otro usuario.
        if (existingUserWithEmail != null && existingUserWithEmail.Id != id)
        {
            throw new InvalidOperationException("Email address already exists.");
        }

        // Validar si facultad no existe.
        if (await _facultadRepository.GetByIdAsync(updateUserDto.Codigofacultad) == null)
        {
            throw new InvalidOperationException("Codigofacultad doesn't exist.");
        }

        UserDto updateUser = new
        (
            Id: id,
            updateUserDto.Email,
            updateUserDto.Passwd,
            updateUserDto.Nombre,
            updateUserDto.Apellido,
            updateUserDto.RoleId,
            updateUserDto.Codigofacultad,
            CreatedAd: existingUser.CreatedAd,
            UpdatedAt: DateTime.Now
        );

        User userEntity = _userMapper.MapToUser(updateUser);

        bool result = await _userRepository.EditUserAsync(id, userEntity);
        await SaveChangesAsync();

        return result;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        User? user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException($"No se encontró ningun usuario con el id {id}.");
        }

        await _userRepository.DeleteUserAsync(user);
        await SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _userRepository.SaveChangesAsync();
    }
}
