using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Services.Contracts;

public interface IAuthService
{
    Task<User> RegisterUserAsync(RegisterUserDto registerUserDto);
    Task<string> Singin(SigninDto signinDto);
    Task SaveChangesAsync();
}
