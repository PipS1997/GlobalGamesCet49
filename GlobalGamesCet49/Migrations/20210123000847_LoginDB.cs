using Microsoft.EntityFrameworkCore.Migrations;

namespace GlobalGamesCet49.Migrations
{
    public partial class LoginDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apelido",
                table: "Inscricoes");

            migrationBuilder.RenameColumn(
                name: "Morada",
                table: "Inscricoes",
                newName: "Email");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Inscricoes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Localidade",
                table: "Inscricoes",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Localidade",
                table: "Inscricoes");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Inscricoes",
                newName: "Morada");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Inscricoes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Apelido",
                table: "Inscricoes",
                nullable: true);
        }
    }
}
