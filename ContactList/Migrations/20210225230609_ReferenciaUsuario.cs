using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactList.Migrations
{
    public partial class ReferenciaUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioNome",
                table: "Contato",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioNome",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioNome",
                table: "Contato");

            migrationBuilder.DropColumn(
                name: "UsuarioNome",
                table: "Categorias");
        }
    }
}
