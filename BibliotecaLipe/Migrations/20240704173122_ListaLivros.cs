using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaLipe.Migrations
{
    /// <inheritdoc />
    public partial class ListaLivros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrinhos_Livros_Livroid",
                table: "Carrinhos");

            migrationBuilder.DropForeignKey(
                name: "FK_ListaDesejos_Livros_Livroid",
                table: "ListaDesejos");

            migrationBuilder.DropIndex(
                name: "IX_ListaDesejos_Livroid",
                table: "ListaDesejos");

            migrationBuilder.DropIndex(
                name: "IX_Carrinhos_Livroid",
                table: "Carrinhos");

            migrationBuilder.DropColumn(
                name: "Livroid",
                table: "ListaDesejos");

            migrationBuilder.DropColumn(
                name: "Livroid",
                table: "Carrinhos");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "Livroid",
                table: "ListaDesejos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Livroid",
                table: "Carrinhos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ListaDesejos_Livroid",
                table: "ListaDesejos",
                column: "Livroid");

            migrationBuilder.CreateIndex(
                name: "IX_Carrinhos_Livroid",
                table: "Carrinhos",
                column: "Livroid");

            migrationBuilder.AddForeignKey(
                name: "FK_Carrinhos_Livros_Livroid",
                table: "Carrinhos",
                column: "Livroid",
                principalTable: "Livros",
                principalColumn: "LivroID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListaDesejos_Livros_Livroid",
                table: "ListaDesejos",
                column: "Livroid",
                principalTable: "Livros",
                principalColumn: "LivroID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
