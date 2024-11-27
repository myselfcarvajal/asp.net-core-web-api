using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AwpaAcademic.Api.Exceptions;
using AwpaAcademic.Api.Mappers.Contracts;
using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;
using AwpaAcademic.Api.Repositories.Contracts;
using AwpaAcademic.Api.Services.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace AwpaAcademic.Api.Services;

public class AuthService : IAuthService
{
    public readonly IFacultadRepository _facultadRepository;
    public readonly IAuthRepository _authRepository;
    public readonly IUserRepository _userRepository;
    public readonly IUserMapper _userMapper;
    public readonly IConfiguration _configuration;

    public AuthService(
        IAuthRepository authRepository,
        IUserRepository userRepository,
        IFacultadRepository facultadRepository,
        IUserMapper userMapper,
        IConfiguration configuration)
    {
        _authRepository = authRepository;
        _userRepository = userRepository;
        _facultadRepository = facultadRepository;
        _userMapper = userMapper;
        _configuration = configuration;
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

    public async Task<string> Singin(SigninDto signinDto)
    {
        var user = await _userRepository.GetByEmailAsync(signinDto.Email);
        if (user == null) throw new UnauthorizedException("Invalid credentials");

        if (user.Passwd != signinDto.Passwd)
        {
            throw new UnauthorizedException("Invalid credentials");
        }

        var token = CreateToken(
            user.Id,
            user.Email,
            user.Codigofacultad,
            user.RoleId);

        return token;
    }

    private string CreateToken(
        int id,
        string email,
        string codigofacultad,
        int roleId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, roleId.ToString()),
                new Claim("CodigoFacultad", codigofacultad ?? string.Empty), // Claim personalised
            }),
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(token);
    }

    public async Task SaveChangesAsync()
    {
        await _authRepository.SaveChangesAsync();
    }
}
