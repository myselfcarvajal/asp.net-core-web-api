using AwpaAcademic.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AwpaAcademic.Api.Data;

public class AwpaAcademicDbContext(DbContextOptions<AwpaAcademicDbContext> options) 
    : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Facultad> Facultades { get; set; }
    public DbSet<Publicacion> Publicaciones { get; set; }
    public DbSet<Role> Roles { get; set; }
}
