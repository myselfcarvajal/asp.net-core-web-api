using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AwpaAcademic.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facultades",
                columns: table => new
                {
                    CodigoFacultad = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    NombreFacultad = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facultades", x => x.CodigoFacultad);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Passwd = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Codigofacultad = table.Column<string>(type: "nvarchar(8)", nullable: true),
                    CreatedAd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Facultades_Codigofacultad",
                        column: x => x.Codigofacultad,
                        principalTable: "Facultades",
                        principalColumn: "CodigoFacultad");
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Publicaciones",
                columns: table => new
                {
                    IdPublicacion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CodigoFacultad = table.Column<string>(type: "nvarchar(8)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicaciones", x => x.IdPublicacion);
                    table.ForeignKey(
                        name: "FK_Publicaciones_Facultades_CodigoFacultad",
                        column: x => x.CodigoFacultad,
                        principalTable: "Facultades",
                        principalColumn: "CodigoFacultad",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Publicaciones_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Facultades",
                columns: new[] { "CodigoFacultad", "NombreFacultad" },
                values: new object[,]
                {
                    { "ACE", "Administrativas, Contables y Económicas" },
                    { "BA", "Bellas Artes" },
                    { "CBE", "Ciencias Básicas y de la Educación" },
                    { "CS", "Ciencias de la Salud" },
                    { "DCPS", "Derecho, Ciencias Políticas y Sociales" },
                    { "IT", "Ingenierías Tecnológicas" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ADMIN" },
                    { 2, "DOCENTE" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Apellido", "Codigofacultad", "CreatedAd", "Email", "Nombre", "Passwd", "RoleId", "UpdatedAt" },
                values: new object[,]
                {
                    { 432512321, "Lopez", null, new DateTime(2024, 7, 22, 22, 31, 3, 739, DateTimeKind.Local).AddTicks(1129), "Mario@gmail.com", "Mario", "Passwd", 1, new DateTime(2024, 7, 22, 22, 31, 3, 739, DateTimeKind.Local).AddTicks(1130) },
                    { 1423423431, "Dickinson", "DCPS", new DateTime(2024, 7, 22, 22, 31, 3, 739, DateTimeKind.Local).AddTicks(1126), "kaitlyn.dickinson81@gmail.com", "Kaitlyn", "Passwd", 2, new DateTime(2024, 7, 22, 22, 31, 3, 739, DateTimeKind.Local).AddTicks(1127) },
                    { 1902583458, "Hane-Willms", "ACE", new DateTime(2024, 7, 22, 22, 31, 3, 739, DateTimeKind.Local).AddTicks(1122), "kevin.hane-willms@gmail.com", "Kevin", "Passwd", 2, new DateTime(2024, 7, 22, 22, 31, 3, 739, DateTimeKind.Local).AddTicks(1123) },
                    { 2147483647, "Dickinson", "IT", new DateTime(2024, 7, 22, 22, 31, 3, 739, DateTimeKind.Local).AddTicks(1106), "shaniya_dickinson@gmail.com", "Shaniya", "Passwd", 2, new DateTime(2024, 7, 22, 22, 31, 3, 739, DateTimeKind.Local).AddTicks(1119) }
                });

            migrationBuilder.InsertData(
                table: "Publicaciones",
                columns: new[] { "IdPublicacion", "Autor", "CodigoFacultad", "CreatedAt", "Descripcion", "Titulo", "UpdatedAt", "Url", "UserId" },
                values: new object[,]
                {
                    { new Guid("1430a275-bcea-4499-9075-cdea11b4ae05"), "Darien Raynor", "DCPS", new DateTime(2024, 7, 22, 22, 31, 3, 740, DateTimeKind.Local).AddTicks(2191), "emplum tenetur confero cupio copia verbera solvo a corrupti deputo. Constans audacia torrens paens aduro. Chirographum traho confido convoco cupressus aeger amet.", "dignissimos toties cenaculum", new DateTime(2024, 7, 22, 22, 31, 3, 740, DateTimeKind.Local).AddTicks(2192), "https://that-clarinet.biz", 1423423431 },
                    { new Guid("7a621b34-03d7-456f-a2eb-f95ece257c60"), "Wilfredo Douglas", "ACE", new DateTime(2024, 7, 22, 22, 31, 3, 740, DateTimeKind.Local).AddTicks(2186), "Adflicto corona curtus conspergo velum paulatim ea solitudo. Ancilla ipsam charisma deporto accusantium aureus earum. Spiritus verumtamen aptus temeritas creber tredecim.", "possimus uterque curis", new DateTime(2024, 7, 22, 22, 31, 3, 740, DateTimeKind.Local).AddTicks(2187), "https://unwelcome-circulation.net/", 1902583458 },
                    { new Guid("8da5c671-5da9-4d2d-92a1-80f7838b72e9"), "Alanna Little", "IT", new DateTime(2024, 7, 22, 22, 31, 3, 740, DateTimeKind.Local).AddTicks(2170), "olo terreo audacia aqua. Sono vulgus viduo. Synagoga textor aestas odit cito deduco.", "bduco carcer incidunt", new DateTime(2024, 7, 22, 22, 31, 3, 740, DateTimeKind.Local).AddTicks(2182), "https://ashamed-neonate.net/", 2147483647 },
                    { new Guid("cf6f65fe-85ef-4047-95db-107903d10bc4"), "Bruce Stoltenberg", "IT", new DateTime(2024, 7, 22, 22, 31, 3, 740, DateTimeKind.Local).AddTicks(2195), "Corporis denuo creo vacuus crepusculum corrigo deputo aptus. Desipio conqueror doloribus celebrer somniculosus officia usque quis. Tenuis vomica solvo comburo.", "est thorax conduco", new DateTime(2024, 7, 22, 22, 31, 3, 740, DateTimeKind.Local).AddTicks(2196), "https://deserted-rainbow.name", 2147483647 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facultades_CodigoFacultad",
                table: "Facultades",
                column: "CodigoFacultad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publicaciones_CodigoFacultad",
                table: "Publicaciones",
                column: "CodigoFacultad");

            migrationBuilder.CreateIndex(
                name: "IX_Publicaciones_UserId",
                table: "Publicaciones",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Id",
                table: "Roles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Codigofacultad",
                table: "Users",
                column: "Codigofacultad");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id",
                table: "Users",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Publicaciones");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Facultades");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
