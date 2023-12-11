using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeguridadInformática.Migrations
{
    public partial class ajustesTercera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RiesgoPorActivo_Id_Activo",
                table: "RiesgoPorActivo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RiesgoPorActivo_Id_Activo",
                table: "RiesgoPorActivo",
                column: "Id_Activo");
        }
    }
}
