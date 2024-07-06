using Biblioteca.Data;
using Biblioteca.Models;
using Biblioteca.Models.Enums;

namespace BibliotecaLipe.Models.Strategy;

public class CategoriesConcret : IStrategy
{
    public BibliotecaDbContext _context { get; set; }

    public CategoriesConcret(BibliotecaDbContext context)
    {
        _context = context;
    }
    public List<Livro> ListCategory(Categorias categoria)
    {
        return _context.Livros.Where(x => x.Categoria == categoria).ToList();
    }
}