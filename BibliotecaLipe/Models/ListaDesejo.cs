using Biblioteca.Models;
using BibliotecaLipe.Models;
using Microsoft.AspNetCore.Identity;

public class ListaDesejo
{
    public int Id { get; set; }

    public List<Livro> Livros { get; set; } = new List<Livro>();

    public string UsuarioId { get; set; }

    public IdentityUser Usuario { get; set; }
}