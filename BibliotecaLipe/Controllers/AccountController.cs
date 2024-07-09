using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaLipe.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly BibliotecaDbContext _context;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, BibliotecaDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
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

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }
}