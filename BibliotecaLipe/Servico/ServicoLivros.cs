using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Servico;

public class ServicoLivros
{
    private readonly BibliotecaDbContext _context;

    public ServicoLivros(BibliotecaDbContext context)
    {
        this._context = context;
    }

    public IList<Livro> GetAllLivros()
    {
        return _context.Livros.Include(x => x.Emprestimos).ToList();
    }

    public Livro GetLivroById(int id)
    {
        return _context.Livros.FirstOrDefault(x => x.LivroID == id);
    }

    public void Create(Livro livro)
    {
        if (!_context.Livros.Any(x => x.LivroID == livro.LivroID))
        {
                _context.Livros.Add(livro);
                _context.SaveChanges();
        }
    }

    public void Edit(Livro livro)
    {
        if (LivroExist(livro.LivroID))
        {
            _context.Livros.Update(livro);
            _context.SaveChanges();
        }
        else
        {
            throw new Exception("Livro não encontrado");
        }
    }

    public void Remove(int id)
    {
        var LivroRemover = GetLivroById(id);
        if (LivroRemover != null)
        {
            _context.Livros.Remove(LivroRemover);
            _context.SaveChanges();
        }
    }

    private bool LivroExist(int id)
    {
        return _context.Livros.Any(x => x.LivroID == id);
    }
}