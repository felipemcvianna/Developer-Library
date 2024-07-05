using Biblioteca.Data;
using Biblioteca.Servico;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaLipe.Controllers;

public class ListaDesejoController : Controller
{
    private readonly ServicoListaDesejo _servicoListaDesejo;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly BibliotecaDbContext _context;
    

    public ListaDesejoController(ServicoListaDesejo servicoListaDesejo, UserManager<IdentityUser> userManager, BibliotecaDbContext context)
    {
        _servicoListaDesejo = servicoListaDesejo;
        _userManager = userManager;
        _context = context;
        
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        var listaDesejo = _servicoListaDesejo.GetAllListaDesejo(user.Id);
        return View(listaDesejo);
    }

    [HttpGet]
    public async Task<IActionResult> Create(int? livroId)
    {
        var livro = _context.Livros.FirstOrDefault(x => x.LivroID == livroId);
        if (livro == null)
        {
            return NotFound();
        }
       
        return View(livro);
    }

    [HttpPost]
    public async Task<IActionResult> Create(int livroId)
    {
        var livro = _context.Livros.FirstOrDefault(x => x.LivroID == livroId);
        if (livro == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        _servicoListaDesejo.AddLivroLista(livro, user.Id);

        return RedirectToAction("Index");
    }

}