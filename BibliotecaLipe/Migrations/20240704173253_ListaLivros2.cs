using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaLipe.Migrations
{
    /// <inheritdoc />
    public partial class ListaLivros2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Carrinhos_CarrinhoId",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Livros_CarrinhoId",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "CarrinhoId",
                table: "Livros");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarrinhoId",
                table: "Livros",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Livros_CarrinhoId",
                table: "Livros",
                column: "CarrinhoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Carrinhos_CarrinhoId",
                table: "Livros",
                column: "CarrinhoId",
                principalTable: "Carrinhos",
                principalColumn: "Id");
        }
    }
}
