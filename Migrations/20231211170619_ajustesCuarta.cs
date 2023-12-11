using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeguridadInformática.Migrations
{
    public partial class ajustesCuarta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dimension");

            migrationBuilder.CreateTable(
                name: "ControlPorRiesgo",
                columns: table => new
                {
                    Id_Riesgo = table.Column<int>(type: "int", nullable: false),
                    Id_Control = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlPorRiesgo", x => new { x.Id_Control, x.Id_Riesgo });
                    table.ForeignKey(
                        name: "FK_ControlPorRiesgo_Control_Id_Control",
                        column: x => x.Id_Control,
                        principalTable: "Control",
                        principalColumn: "Id_Control",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ControlPorRiesgo_Riesgo_Id_Riesgo",
                        column: x => x.Id_Riesgo,
                        principalTable: "Riesgo",
                        principalColumn: "Id_Riesgo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControlPorRiesgo_Id_Riesgo",
                table: "ControlPorRiesgo",
                column: "Id_Riesgo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControlPorRiesgo");

            migrationBuilder.CreateTable(
                name: "Dimension",
                columns: table => new
                {
                    Id_Dimension = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimension", x => x.Id_Dimension);
                });
        }
    }
}
