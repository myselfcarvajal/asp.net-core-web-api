using AwpaAcademic.Api.Models.Dtos;
using AwpaAcademic.Api.Models.Entities;

namespace AwpaAcademic.Api.Services.Contracts;

public interface IUserService
{
    public List<UserDto> GetAll();
    public UserDto? GetById(int id);

    public User AddUser(UserDto userDto);
    public bool DeleteUser(int id);
    public bool EditUser(int Id, UserDto userDto);

    public void SaveChanges();
}
