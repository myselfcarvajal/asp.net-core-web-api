using AwpaAcademic.Api.Exceptions;
using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;
using AwpaAcademic.Api.Repositories.Contracts;
using AwpaAcademic.Api.Services.Contracts;

namespace AwpaAcademic.Api.Services;

public class AuthService : IAuthService
{
    public readonly IFacultadRepository _facultadRepository;
    public readonly IAuthRepository _authRepository;
    public readonly IUserRepository _userRepository;
    public readonly IUserMapper _userMapper;

    public AuthService(
        IAuthRepository authRepository,
        IUserRepository userRepository,
        IFacultadRepository facultadRepository,
        IUserMapper userMapper
    )
    {
        _authRepository = authRepository;
        _userRepository = userRepository;
        _facultadRepository = facultadRepository;
        _userMapper = userMapper;
    }

    public async Task<User> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        if (await _userRepository.ExistsAsync(registerUserDto.Id))
        {
            throw new InvalidOperationException("User id already exists.");
        }

        if (await _userRepository.GetByEmailAsync(registerUserDto.Email) != null)
        {
            throw new InvalidOperationException("Email already exists.");
        }

        if (await _facultadRepository.GetByIdAsync(registerUserDto.Codigofacultad) == null)
        {
            throw new InvalidOperationException("Codigofacultad doesn't exist.");
        }

        UserDto newUser = new
        (
            registerUserDto.Id,
            registerUserDto.Email,
            registerUserDto.Passwd,
            registerUserDto.Nombre,
            registerUserDto.Apellido,
            registerUserDto.RoleId,
            registerUserDto.Codigofacultad,
            DateTime.Now,
            DateTime.Now
        );

        User registerUser = _userMapper.MapToUser(newUser);

        await _authRepository.RegisterUserAsync(registerUser);
        await SaveChangesAsync();

        return registerUser;
    }

    public async Task<bool> Singin(SigninDto signinDto)
    {
        var user = await _userRepository.GetByEmailAsync(signinDto.Email);
        if (user == null) throw new UnauthorizedException("Invalid credentials");

        if (user.Passwd != signinDto.Passwd)
        {
            throw new UnauthorizedException("Invalid credentials");
        }

        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _authRepository.SaveChangesAsync();
    }
}
