using System.ComponentModel.DataAnnotations;
using Biblioteca.Models;

namespace BibliotecaLipe.Models;

public class LivroEmprestimoViewModel
{
    public Livro Livro { get; set; }
    public Emprestimo Emprestimo { get; set; }
}