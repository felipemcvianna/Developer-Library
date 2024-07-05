using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaLipe.Migrations
{
    /// <inheritdoc />
    public partial class CarrinhoListaDesejos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarrinhoId",
                table: "Livros",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ListaDesejoId",
                table: "Livros",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Carrinhos",
                columns: table => new
                {
                    CarrinhoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrinhos", x => x.CarrinhoId);
                    table.ForeignKey(
                        name: "FK_Carrinhos_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ListaDesejos",
                columns: table => new
                {
                    ListaDesejoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaDesejos", x => x.ListaDesejoId);
                    table.ForeignKey(
                        name: "FK_ListaDesejos_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_CarrinhoId",
                table: "Livros",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_ListaDesejoId",
                table: "Livros",
                column: "ListaDesejoId");

            migrationBuilder.CreateIndex(
                name: "IX_Carrinhos_UsuarioId",
                table: "Carrinhos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ListaDesejos_UsuarioId",
                table: "ListaDesejos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Carrinhos_CarrinhoId",
                table: "Livros",
                column: "CarrinhoId",
                principalTable: "Carrinhos",
                principalColumn: "CarrinhoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_ListaDesejos_ListaDesejoId",
                table: "Livros",
                column: "ListaDesejoId",
                principalTable: "ListaDesejos",
                principalColumn: "ListaDesejoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Carrinhos_CarrinhoId",
                table: "Livros");

            migrationBuilder.DropForeignKey(
                name: "FK_Livros_ListaDesejos_ListaDesejoId",
                table: "Livros");

            migrationBuilder.DropTable(
                name: "Carrinhos");

            migrationBuilder.DropTable(
                name: "ListaDesejos");

            migrationBuilder.DropIndex(
                name: "IX_Livros_CarrinhoId",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Livros_ListaDesejoId",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "CarrinhoId",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "ListaDesejoId",
                table: "Livros");
        }
    }
}
