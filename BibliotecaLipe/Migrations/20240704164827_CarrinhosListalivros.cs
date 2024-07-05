using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaLipe.Migrations
{
    /// <inheritdoc />
    public partial class CarrinhosListalivros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrinhos_AspNetUsers_UsuarioId",
                table: "Carrinhos");

            migrationBuilder.DropForeignKey(
                name: "FK_ListaDesejos_AspNetUsers_UsuarioId",
                table: "ListaDesejos");

            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Carrinhos_CarrinhoId",
                table: "Livros");

            migrationBuilder.DropForeignKey(
                name: "FK_Livros_ListaDesejos_ListaDesejoId",
                table: "Livros");

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

            migrationBuilder.RenameColumn(
                name: "ListaDesejoId",
                table: "ListaDesejos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CarrinhoId",
                table: "Carrinhos",
                newName: "Id");

            migrationBuilder.UpdateData(
                table: "ListaDesejos",
                keyColumn: "UsuarioId",
                keyValue: null,
                column: "UsuarioId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "ListaDesejos",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Carrinhos",
                keyColumn: "UsuarioId",
                keyValue: null,
                column: "UsuarioId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Carrinhos",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Carrinhos_AspNetUsers_UsuarioId",
                table: "Carrinhos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListaDesejos_AspNetUsers_UsuarioId",
                table: "ListaDesejos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrinhos_AspNetUsers_UsuarioId",
                table: "Carrinhos");

            migrationBuilder.DropForeignKey(
                name: "FK_ListaDesejos_AspNetUsers_UsuarioId",
                table: "ListaDesejos");

            migrationBuilder.DropTable(
                name: "CarrinhoLivros");

            migrationBuilder.DropTable(
                name: "ListaDesejoLivros");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ListaDesejos",
                newName: "ListaDesejoId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Carrinhos",
                newName: "CarrinhoId");

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

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "ListaDesejos",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Carrinhos",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_CarrinhoId",
                table: "Livros",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_ListaDesejoId",
                table: "Livros",
                column: "ListaDesejoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carrinhos_AspNetUsers_UsuarioId",
                table: "Carrinhos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ListaDesejos_AspNetUsers_UsuarioId",
                table: "ListaDesejos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

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
    }
}
