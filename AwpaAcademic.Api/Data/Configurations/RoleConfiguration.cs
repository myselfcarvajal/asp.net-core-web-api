using AwpaAcademic.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwpaAcademic.Api.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(r => r.Id)
            .ValueGeneratedNever();

        builder.HasIndex(r => r.Id)
            .IsUnique();

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(40);
        List<Role> roles = [
            new Role {
                Id = 1,
                Name = "ADMIN"
            },
            new Role {
                Id = 2,
                Name = "DOCENTE"
            }
        ];

        builder.HasData(roles);
    }
}
