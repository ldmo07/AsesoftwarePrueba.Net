using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Direccion",
                columns: table => new
                {
                    id_direccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    barrio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    calle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direccion", x => x.id_direccion);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    id_cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom_cliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ape_cliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nid_cliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_direccion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.id_cliente);
                    table.ForeignKey(
                        name: "FK_Cliente_Direccion_id_direccion",
                        column: x => x.id_direccion,
                        principalTable: "Direccion",
                        principalColumn: "id_direccion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_id_direccion",
                table: "Cliente",
                column: "id_direccion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Direccion");
        }
    }
}
