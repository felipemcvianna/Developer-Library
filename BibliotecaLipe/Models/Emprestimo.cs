using BibliotecaLipe.Models;
using BibliotecaLipe.Models.Enums;
using Microsoft.AspNetCore.Identity;

public class Emprestimo
{
    public int EmprestimoId { get; set; }
    public DateTime DataEmprestimo { get; set; } = DateTime.Now.Date;

    public DateTime DataDevolucao { get; set; }

    public Status Status { get; set; }
    public string UsuarioId { get; set; }
    public IdentityUser Usuario { get; set; }
    public int LivroId { get; set; }
    public Livro Livro { get; set; }

    public Emprestimo()
    {
        DataDevolucao = DataEmprestimo.AddDays(30);
    }
}