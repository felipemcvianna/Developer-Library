using Biblioteca.Data;
using Biblioteca.Models;
using Biblioteca.Servico;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaLipe.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly BibliotecaDbContext _context;
    private readonly ServicoListaDesejo _servicoListaDesejo;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        BibliotecaDbContext context, ServicoListaDesejo servicoListaDesejo)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _servicoListaDesejo = servicoListaDesejo;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser
            {
                UserName = model.Nome,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Usuario");
                await _signInManager.SignInAsync(user, isPersistent: false);
                var listaDesejo = new ListaDesejo
                {
                    UsuarioId = user.Id,
                    Usuario = user
                };
                _context.ListaDesejos.Add(listaDesejo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Livro");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Nome, model.Password, model.RemeberMe, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Nome);
                var listaDesejo = _context.ListaDesejos.FirstOrDefault(x => x.UsuarioId == user.Id);
                if (listaDesejo == null)
                {
                    listaDesejo = new ListaDesejo
                    {
                        UsuarioId = user.Id,
                        Usuario = user
                    };
                    _context.ListaDesejos.Add(listaDesejo);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index", "Livro");
            }

            ModelState.AddModelError(string.Empty, "Login inválido");
        }

        return View(model);
    }

    public async Task<IActionResult> LoginDemo()
    {
        var demoUser = await _userManager.FindByNameAsync("Convidado");
        if (demoUser == null)
        {
            demoUser = new IdentityUser
            {
                UserName = "Convidado",
                Email = "convidado@convidado.com",
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(demoUser, "Convidado123!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(demoUser, "Convidado");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        var listaDesejoExistente = await _context.ListaDesejos
            .FirstOrDefaultAsync(ld => ld.UsuarioId == demoUser.Id);
        if (listaDesejoExistente == null)
        {
            var listaDesejo = new ListaDesejo
            {
                UsuarioId = demoUser.Id,
                Usuario = demoUser
            };

            _context.ListaDesejos.Add(listaDesejo);
            await _context.SaveChangesAsync();
        }

        await _signInManager.SignInAsync(demoUser, isPersistent: false);
        return RedirectToAction("Index", "Livro");
    }

    public async Task<IActionResult> Logout()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Challenge();
        }

        if (user.UserName == "Convidado")
        {
            var lista = await _context.ListaDesejos
                .Include(ld => ld.Livros)
                .FirstOrDefaultAsync(x => x.UsuarioId == user.Id);

            if (lista != null)
            {
                var livrosToRemove = lista.Livros.ToList();
                if (livrosToRemove.Any())
                {
                    foreach (var livro in livrosToRemove)
                    {
                        _servicoListaDesejo.Remove(user.Id, livro.LivroID);
                    }
                }
            }
        }
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }
}