using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaLipe.Migrations
{
    /// <inheritdoc />
    public partial class RemoveListaLivros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoLivros");

            migrationBuilder.DropTable(
                name: "ListaDesejoLivros");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "CarrinhoLivros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CarrinhoId = table.Column<int>(type: "int", nullable: false),
                    LivroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoLivros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrinhoLivros_Carrinhos_CarrinhoId",
                        column: x => x.CarrinhoId,
                        principalTable: "Carrinhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrinhoLivros_Livros_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livros",
                        principalColumn: "LivroID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ListaDesejoLivros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ListaDesejoId = table.Column<int>(type: "int", nullable: false),
                    LivroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaDesejoLivros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListaDesejoLivros_ListaDesejos_ListaDesejoId",
                        column: x => x.ListaDesejoId,
                        principalTable: "ListaDesejos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListaDesejoLivros_Livros_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livros",
                        principalColumn: "LivroID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoLivros_CarrinhoId",
                table: "CarrinhoLivros",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoLivros_LivroId",
                table: "CarrinhoLivros",
                column: "LivroId");

            migrationBuilder.CreateIndex(
                name: "IX_ListaDesejoLivros_ListaDesejoId",
                table: "ListaDesejoLivros",
                column: "ListaDesejoId");

            migrationBuilder.CreateIndex(
                name: "IX_ListaDesejoLivros_LivroId",
                table: "ListaDesejoLivros",
                column: "LivroId");
        }
    }
}
