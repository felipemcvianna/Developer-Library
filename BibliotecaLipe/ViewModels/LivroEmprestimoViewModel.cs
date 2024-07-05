using System.ComponentModel.DataAnnotations.Schema;
using Biblioteca.Models;

namespace BibliotecaLipe.Models;

public class LivroEmprestimoViewModel
{
    [NotMapped]
    public Livro Livro { get; set; }
    public Emprestimo Emprestimo { get; set; }
}