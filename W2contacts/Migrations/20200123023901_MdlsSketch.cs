using Microsoft.EntityFrameworkCore.Migrations;

namespace W2contacts.Migrations
{
    public partial class MdlsSketch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Clientes",
                newName: "Telefone");

            migrationBuilder.AddColumn<string>(
                name: "ContatoTecnico",
                table: "Concessionarias",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Concessionarias",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Concessionarias",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Clientes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConcessionariaID",
                table: "Clientes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clientes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Clientes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeEmpresa",
                table: "Clientes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeResponsavel",
                table: "Clientes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RamoAtividade",
                table: "Clientes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ConcessionariaID",
                table: "Clientes",
                column: "ConcessionariaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Concessionarias_ConcessionariaID",
                table: "Clientes",
                column: "ConcessionariaID",
                principalTable: "Concessionarias",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Concessionarias_ConcessionariaID",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_ConcessionariaID",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ContatoTecnico",
                table: "Concessionarias");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Concessionarias");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Concessionarias");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ConcessionariaID",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "NomeEmpresa",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "NomeResponsavel",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "RamoAtividade",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "Clientes",
                newName: "Nome");
        }
    }
}
