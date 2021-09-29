using Microsoft.EntityFrameworkCore.Migrations;

namespace Futbol.Migrations
{
    public partial class Migracion_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JugadoresPorEquipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JugadorId = table.Column<int>(type: "INTEGER", nullable: false),
                    EquipoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JugadoresPorEquipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JugadoresPorEquipos_Equipos_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JugadoresPorEquipos_Jugadores_JugadorId",
                        column: x => x.JugadorId,
                        principalTable: "Jugadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JugadoresPorEquipos_EquipoId",
                table: "JugadoresPorEquipos",
                column: "EquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_JugadoresPorEquipos_JugadorId",
                table: "JugadoresPorEquipos",
                column: "JugadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JugadoresPorEquipos");
        }
    }
}
