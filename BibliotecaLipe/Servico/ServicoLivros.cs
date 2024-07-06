using Biblioteca.Data;
using Biblioteca.Models;
using Biblioteca.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Servico;

public class ServicoLivros
{
    private readonly BibliotecaDbContext _context;
    private readonly Logger<ServicoLivros> _logger;

    public ServicoLivros(BibliotecaDbContext context, Logger<ServicoLivros> logger)
    {
        this._context = context;
        _logger = logger;
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

    public List<Livro> ListCategories(Categorias categoria)
    {
        _logger.LogInformation($"A categoria passada no serviço é {categoria}");
        return _context.Livros.Where(x => x.Categoria == categoria).ToList();
    }


    private bool LivroExist(int id)
    {
        return _context.Livros.Any(x => x.LivroID == id);
    }
}