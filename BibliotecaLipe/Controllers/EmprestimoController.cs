using Biblioteca.Models;
using Biblioteca.Servico;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers;
[Authorize]
public class EmprestimoController : Controller
{
    private readonly ServicoEmprestimo _servicoEmprestimo;

    public EmprestimoController(ServicoEmprestimo servicoEmprestimo)
    {
        _servicoEmprestimo = servicoEmprestimo;
    }

    [HttpGet]
    public IActionResult Index()
    {
        IList<Emprestimo> list = _servicoEmprestimo.GetAllEmprestimos();
        return View(list);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Emprestimo emprestimo)
    {
        _servicoEmprestimo.Create(emprestimo);
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