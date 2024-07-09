using System.Security.Claims;
using Biblioteca.Data;
using Biblioteca.Servico;
using BibliotecaLipe.Models;
using BibliotecaLipe.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaLipe.Controllers;

[Authorize]
public class EmprestimoController : Controller
{
    private readonly ServicoEmprestimo _servicoEmprestimo;
    private readonly BibliotecaDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public EmprestimoController(ServicoEmprestimo servicoEmprestimo, BibliotecaDbContext context,
        UserManager<IdentityUser> userManager)
    {
        _servicoEmprestimo = servicoEmprestimo;
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        IList<Emprestimo> list = _servicoEmprestimo.GetAllEmprestimosByUser(userId);
        foreach (var emprestimo in list)
        {
            if (emprestimo.DataDevolucao < DateTime.Now && emprestimo.Status != Status.Encerrado)
            {
                emprestimo.Status = Status.Vencido;
            }
        }

        return View(list);
    }
    
    [HttpGet]
    public IActionResult Create(int livroId)
    {
        var livro = _context.Livros.FirstOrDefault(x => x.LivroID == livroId);
        if (livro == null)
        {
            return NotFound("Não foi possível encontrar o livro");
        }

        LivroEmprestimoViewModel livroEmprestimo = new()
        {
            Livro = livro,
            Emprestimo = new Emprestimo
            {
                LivroId = livro.LivroID
            }
        };
        return View(livroEmprestimo);
    }

    [HttpPost]
    public async Task<IActionResult> Create(LivroEmprestimoViewModel emprestimoViewModel)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }
        if (emprestimoViewModel.Emprestimo.DataDevolucao < DateTime.Today)
        {
            ModelState.AddModelError(emprestimoViewModel.Emprestimo.DataDevolucao.ToString(),
                "A data de devolução não pode ser anterior à data atual.");
        }
        emprestimoViewModel.Emprestimo.UsuarioId = user.Id;
        _servicoEmprestimo.Create(emprestimoViewModel.Emprestimo);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(string searchString)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var listEmprestimo = _servicoEmprestimo.GetEmprestimosByLivro(user.Id, searchString);
            return View(listEmprestimo);
        }

        return BadRequest();
    }
    [HttpPost]
    public IActionResult Edit(int id)
    {
        var emprestimoExistente = _servicoEmprestimo.GetEmprestimosById(id);
        if (emprestimoExistente == null)
        {
            return BadRequest("Emprestimo não encontrado");
        }

        _servicoEmprestimo.Update(emprestimoExistente);

        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    public IActionResult Remove(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("model state não é valido");
        }

        var emprestimoARemover = _servicoEmprestimo.GetEmprestimosById(id);
        if (emprestimoARemover != null)
        {
            _servicoEmprestimo.Delete(emprestimoARemover.EmprestimoId);
            return RedirectToAction(nameof(Index));
        }

        return BadRequest("Não foi possível localizar o emprestimo");
    }
}