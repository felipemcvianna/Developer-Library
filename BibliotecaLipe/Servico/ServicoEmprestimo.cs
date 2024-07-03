using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Servico;

public class ServicoEmprestimo
{
    private readonly BibliotecaDbContext _context;

    public ServicoEmprestimo(BibliotecaDbContext context)
    {
        _context = context;
    }

    public IList<Emprestimo> GetAllEmprestimosByUser(string id)
    {
        return _context.Emprestimos
            .Include(x => x.Usuario)
            .Include(x => x.Livro).Where(x => x.Usuario.Id == id).ToList();
    }

    public Emprestimo? GetEmprestimosById(int id)
    {
        return _context.Emprestimos
            .Include(x => x.Usuario)
            .Include(x => x.Livro)
            .FirstOrDefault(x => x.EmprestimoId == id);
    }

    public void Create(Emprestimo emprestimo)
    {
        _context.Emprestimos.Add(emprestimo);
        _context.SaveChanges();
    }

    public void Update(Emprestimo emprestimo)
    {
        if (_context.Emprestimos.Any(x => x.EmprestimoId == emprestimo.EmprestimoId))
        {
            _context.Emprestimos.Update(emprestimo);
            _context.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        var EmprestimoRemover = _context.Emprestimos.FirstOrDefault(x => x.EmprestimoId == id);
        if (EmprestimoRemover != null)
            _context.Emprestimos.Remove(EmprestimoRemover);
    }
}