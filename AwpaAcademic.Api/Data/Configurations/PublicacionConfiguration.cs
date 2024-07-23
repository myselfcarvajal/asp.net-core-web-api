using AwpaAcademic.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwpaAcademic.Api.Data.Configurations;

public class PublicacionConfiguration : IEntityTypeConfiguration<Publicacion>
{
    public void Configure(EntityTypeBuilder<Publicacion> builder)
    {
        builder.Property(p => p.Titulo)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Autor)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Descripcion)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.Url)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(p => p.Facultad)
            .WithMany(f => f.Publicaciones)
            .HasForeignKey(p => p.CodigoFacultad)
            .HasConstraintName("FK_Publicaciones_Facultades_CodigoFacultad");

        builder.HasOne(p => p.User)
            .WithMany(u => u.Publicaciones)
            .HasForeignKey(p => p.UserId)
            .HasConstraintName("FK_Publicaciones_Users_UserId");

        List<Publicacion> publicaciones =
        [
            new Publicacion
            {
                IdPublicacion = Guid.NewGuid(),
                Titulo = "bduco carcer incidunt",
                Autor = "Alanna Little",
                Descripcion = "olo terreo audacia aqua. Sono vulgus viduo. Synagoga textor aestas odit cito deduco.",
                Url = "https://ashamed-neonate.net/",
                UserId = 2147483647,
                CodigoFacultad = "IT",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Publicacion
            {
                IdPublicacion =  Guid.NewGuid(),
                Titulo = "possimus uterque curis",
                Autor = "Wilfredo Douglas",
                Descripcion = "Adflicto corona curtus conspergo velum paulatim ea solitudo. Ancilla ipsam charisma deporto accusantium aureus earum. Spiritus verumtamen aptus temeritas creber tredecim.",
                Url = "https://unwelcome-circulation.net/",
                UserId = 1902583458,
                CodigoFacultad = "ACE",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Publicacion
            {
                IdPublicacion =  Guid.NewGuid(),
                Titulo = "dignissimos toties cenaculum",
                Autor = "Darien Raynor",
                Descripcion = "emplum tenetur confero cupio copia verbera solvo a corrupti deputo. Constans audacia torrens paens aduro. Chirographum traho confido convoco cupressus aeger amet.",
                Url = "https://that-clarinet.biz",
                UserId = 1423423431,
                CodigoFacultad = "DCPS",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Publicacion
            {
                IdPublicacion =  Guid.NewGuid(),
                Titulo = "est thorax conduco",
                Autor = "Bruce Stoltenberg",
                Descripcion = "Corporis denuo creo vacuus crepusculum corrigo deputo aptus. Desipio conqueror doloribus celebrer somniculosus officia usque quis. Tenuis vomica solvo comburo.",
                Url = "https://deserted-rainbow.name",
                UserId = 2147483647,
                CodigoFacultad = "IT",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }
        ];

        builder.HasData(publicaciones);

    }
}
