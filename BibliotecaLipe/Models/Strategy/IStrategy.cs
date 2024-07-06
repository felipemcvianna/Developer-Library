using Biblioteca.Data;
using Biblioteca.Models;
using Biblioteca.Models.Enums;

namespace BibliotecaLipe.Models.Strategy;

public interface IStrategy
{
    public List<Livro> ListCategory(Categorias categoria);
    
}