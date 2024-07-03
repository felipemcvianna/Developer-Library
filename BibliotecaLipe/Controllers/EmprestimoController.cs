using System.Security.Claims;
using Biblioteca.Data;
using Biblioteca.Models;
using Biblioteca.Servico;
using BibliotecaLipe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers;

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
            ModelState.AddModelError("Emprestimo.DataDevolucao", "A data de devolução não pode ser anterior à data atual.");
            return View(emprestimoViewModel);
        }

        emprestimoViewModel.Emprestimo.UsuarioId = user.Id;

        _servicoEmprestimo.Create(emprestimoViewModel.Emprestimo);
        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public IActionResult Edit(int id)
    {
        var EmprestimoEdit = _servicoEmprestimo.GetEmprestimosById(id);
        if (EmprestimoEdit == null)
        {
            return BadRequest("Emprestimo não encontrado");
        }

        return View(EmprestimoEdit);
    }

    [HttpPost]
    public IActionResult Edit(int id, Emprestimo emprestimo)
    {
        if (id != emprestimo.EmprestimoId)
        {
            return BadRequest("Não foi possível editar o emprestimo. Ids diferentes");
        }

        if (ModelState.IsValid)
        {
            var EmprestimoExistente = _servicoEmprestimo.GetEmprestimosById(emprestimo.EmprestimoId);
            if (EmprestimoExistente != null)
            {
                _servicoEmprestimo.Update(EmprestimoExistente);
            }
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Remove(int id)
    {
        var EmprestimoRemove = _servicoEmprestimo.GetEmprestimosById(id);
        return View(EmprestimoRemove);
    }

    [HttpPost]
    public IActionResult Remove(Emprestimo emprestimo)
    {
        var emprestimoRemove = _servicoEmprestimo.GetEmprestimosById(emprestimo.EmprestimoId);
        if (emprestimoRemove != null)
            _servicoEmprestimo.Delete(emprestimoRemove.EmprestimoId);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int id)
    {
        var emprestimoDetalhe = _servicoEmprestimo.GetEmprestimosById(id);
        if (emprestimoDetalhe == null)
            return BadRequest("Emprestimo não encontrado");
        return View(emprestimoDetalhe);
    }
}