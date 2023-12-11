using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeguridadInformática.Migrations
{
    public partial class Db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activo",
                columns: table => new
                {
                    Id_Activo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activo", x => x.Id_Activo);
                });

            migrationBuilder.CreateTable(
                name: "Control",
                columns: table => new
                {
                    Id_Control = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Control", x => x.Id_Control);
                });

            migrationBuilder.CreateTable(
                name: "Dimension",
                columns: table => new
                {
                    Id_Dimension = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimension", x => x.Id_Dimension);
                });

            migrationBuilder.CreateTable(
                name: "Riesgo",
                columns: table => new
                {
                    Id_Riesgo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Riesgo", x => x.Id_Riesgo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activo");

            migrationBuilder.DropTable(
                name: "Control");

            migrationBuilder.DropTable(
                name: "Dimension");

            migrationBuilder.DropTable(
                name: "Riesgo");
        }
    }
}
