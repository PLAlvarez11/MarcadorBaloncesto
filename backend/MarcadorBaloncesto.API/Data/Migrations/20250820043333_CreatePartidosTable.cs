using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MarcadorBaloncesto.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatePartidosTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipoLocal = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EquipoVisitante = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PuntosLocal = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    PuntosVisitante = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FaltasLocal = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FaltasVisitante = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FechaPartido = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Partidos",
                columns: new[] { "Id", "EquipoLocal", "EquipoVisitante", "FaltasLocal", "FaltasVisitante", "FechaPartido", "PuntosLocal", "PuntosVisitante" },
                values: new object[,]
                {
                    { 1, "Lakers", "Warriors", 15, 12, new DateTime(2025, 8, 19, 22, 33, 33, 96, DateTimeKind.Local).AddTicks(5739), 110, 105 },
                    { 2, "Bulls", "Celtics", 10, 8, new DateTime(2025, 8, 19, 22, 33, 33, 96, DateTimeKind.Local).AddTicks(5755), 95, 98 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_EquipoLocal",
                table: "Partidos",
                column: "EquipoLocal");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_EquipoVisitante",
                table: "Partidos",
                column: "EquipoVisitante");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partidos");
        }
    }
}
