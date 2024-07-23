using AwpaAcademic.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwpaAcademic.Api.Data.Configurations;

public class FacultadConfiguration : IEntityTypeConfiguration<Facultad>
{
    public void Configure(EntityTypeBuilder<Facultad> builder)
    {
        builder.Property(f => f.CodigoFacultad)
            .ValueGeneratedNever()
            .HasMaxLength(8);
        builder.HasIndex(f => f.CodigoFacultad)
            .IsUnique();
        builder.Property(f => f.NombreFacultad)
            .IsRequired()
            .HasMaxLength(60);
        List<Facultad> facultades =
        [
            new Facultad
            {
                CodigoFacultad = "ACE",
                NombreFacultad = "Administrativas, Contables y Económicas",
            },
            new Facultad
            {
                CodigoFacultad = "DCPS",
                NombreFacultad = "Derecho, Ciencias Políticas y Sociales",
            },
            new Facultad
            {
                CodigoFacultad = "IT",
                NombreFacultad = "Ingenierías Tecnológicas",
            },
            new Facultad
            {
                CodigoFacultad = "BA",
                NombreFacultad = "Bellas Artes",
            },
            new Facultad
            {
                CodigoFacultad = "CBE",
                NombreFacultad = "Ciencias Básicas y de la Educación",
            },
            new Facultad
            {
                CodigoFacultad = "CS",
                NombreFacultad = "Ciencias de la Salud",
            },
        ];

        builder.HasData(facultades);
    }
}
