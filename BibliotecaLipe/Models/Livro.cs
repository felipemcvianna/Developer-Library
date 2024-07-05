using System.ComponentModel.DataAnnotations.Schema;
using Biblioteca.Models.Enums;

namespace Biblioteca.Models;

public class Livro
{
    public int LivroID { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string ISBN { get; set; }
    public Categorias Categoria { get; set; }
    public DateTime DataPublicacao { get; set; }
    public string Descricao { get; set; }
    public string? CaminhoImagem { get; set; }
    public ICollection<Emprestimo> Emprestimos { get; set; } = new List<Emprestimo>();
    [NotMapped] public int QuantidadeEmprestado => Emprestimos.Count;
}