using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Servico;

public class ServicoListaDesejo
{
    private readonly BibliotecaDbContext _context;

    public ServicoListaDesejo(BibliotecaDbContext context)
    {
        _context = context;
    }

    public ListaDesejo? GetAllListaDesejo(string id)
    {
        return _context.ListaDesejos
            .Include(x => x.Livros)
            .Include(x => x.Usuario)
            .FirstOrDefault(x => x.UsuarioId == id);
    }


    public void AddLivroLista(Livro livro, string id)
    {
        if (!LivroExiste(livro))
        {
            throw new ArgumentException("Livro não encontrado no banco de dados.");
        }

        var listaDesejo = GetAllListaDesejo(id);
        if (listaDesejo == null)
        {
            throw new ArgumentNullException("Não foi possível encontrar a lista de desejo");
        }

        if (listaDesejo.Livros.All(x => x.LivroID != livro.LivroID))
        {
            listaDesejo.Livros.Add(livro);
            Update(listaDesejo);
        }
    }

    public void Remove(string IdUusuario, int livroId)
    {
        ListaDesejo? listaDesejo = GetAllListaDesejo(IdUusuario);
        if (listaDesejo == null)
        {
            return;
        }

        var LivroARemover = listaDesejo.Livros.FirstOrDefault(x => x.LivroID == livroId);
        if (LivroARemover != null)
        {
            listaDesejo.Livros.Remove(LivroARemover);
            Update(listaDesejo);
        }
    }


    public void Update(ListaDesejo listaDesejo)
    {
        _context.Update(listaDesejo);
        _context.SaveChanges();
    }

    private bool LivroExiste(Livro livro)
    {
        return _context.Livros.Any(x => x.LivroID == livro.LivroID);
    }
}