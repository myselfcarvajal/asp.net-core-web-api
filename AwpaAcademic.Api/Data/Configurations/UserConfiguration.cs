using AwpaAcademic.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwpaAcademic.Api.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Id)
            .ValueGeneratedNever();

        builder.HasIndex(u => u.Id)
            .IsUnique();

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Passwd)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.Nombre)
            .IsRequired()
            .HasMaxLength(80);

        builder.Property(u => u.Apellido)
            .IsRequired()
            .HasMaxLength(80);

        builder.HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId)
            .HasConstraintName("FK_Users_Roles_RoleId");


        builder.HasOne(u => u.Facultad)
            .WithMany()
            .HasForeignKey(u => u.Codigofacultad)
            .HasConstraintName("FK_Users_Facultades_Codigofacultad");

        List<User> users = [
            new User {
                Id=  2147483647,
                Email = "shaniya_dickinson@gmail.com",
                Passwd= "Passwd",
                Nombre = "Shaniya",
                Apellido = "Dickinson",
                RoleId = 2,
                Codigofacultad = "IT",
                CreatedAd = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new User {
                Id=  1902583458,
                Email= "kevin.hane-willms@gmail.com",
                Passwd= "Passwd",
                Nombre = "Kevin",
                Apellido = "Hane-Willms",
                RoleId = 2,
                Codigofacultad = "ACE",
                CreatedAd = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new User {
                Id=  1423423431,
                Email= "kaitlyn.dickinson81@gmail.com",
                Passwd= "Passwd",
                Nombre = "Kaitlyn",
                Apellido = "Dickinson",
                RoleId = 2,
                Codigofacultad = "DCPS",
                CreatedAd = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new User {
                Id=  432512321,
                Email= "Mario@gmail.com",
                Passwd= "Passwd",
                Nombre = "Mario",
                Apellido = "Lopez",
                RoleId = 1,
                CreatedAd = DateTime.Now,
                UpdatedAt = DateTime.Now
            }
        ];

        builder.HasData(users);
    }
}

