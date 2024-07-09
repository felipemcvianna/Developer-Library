using BibliotecaLipe.Models.Enums;

namespace BibliotecaLipe.Models.Strategy;

public class ContextStrategy
{
    private IStrategy Strategy { get; set; }
    
    public ContextStrategy(IStrategy strategy)
    {
        Strategy = strategy;
    }

    public void SetStrategy(IStrategy strategy)
    {
        this.Strategy = strategy;
    }
    public List<Livro> RetornarList(Categorias categorias)
    {
        return Strategy.ListCategory(categorias);
    }
    
}