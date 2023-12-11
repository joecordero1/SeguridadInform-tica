using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeguridadInformática.Migrations
{
    public partial class segunda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RiesgoPorActivo",
                columns: table => new
                {
                    Id_Activo = table.Column<int>(type: "int", nullable: false),
                    Id_Riesgo = table.Column<int>(type: "int", nullable: false),
                    ActivoId_Activo = table.Column<int>(type: "int", nullable: false),
                    RiesgoId_Riesgo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiesgoPorActivo", x => new { x.Id_Activo, x.Id_Riesgo });
                    table.ForeignKey(
                        name: "FK_RiesgoPorActivo_Activo_ActivoId_Activo",
                        column: x => x.Id_Activo,
                        principalTable: "Activo",
                        principalColumn: "Id_Activo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RiesgoPorActivo_Riesgo_RiesgoId_Riesgo",
                        column: x => x.Id_Riesgo,
                        principalTable: "Riesgo",
                        principalColumn: "Id_Riesgo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RiesgoPorActivo_ActivoId_Activo",
                table: "RiesgoPorActivo",
                column: "Id_Activo");

            migrationBuilder.CreateIndex(
                name: "IX_RiesgoPorActivo_RiesgoId_Riesgo",
                table: "RiesgoPorActivo",
                column: "Id_Riesgo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RiesgoPorActivo");
        }
    }
}
