using Biblioteca.Servico.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Biblioteca.Servico;

public class SeedUserRoleInitial : ISeedUserRoleInitial
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedRolesAsync()
    {
        if (!await _roleManager.RoleExistsAsync("Usuario"))
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Usuario";
            role.NormalizedName = "USUARIO";
            role.ConcurrencyStamp = Guid.NewGuid().ToString();
            IdentityResult result = await _roleManager.CreateAsync(role);
        }

        if (!await _roleManager.RoleExistsAsync("Administrador"))
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Administrador";
            role.NormalizedName = "ADMINISTRADOR";
            role.ConcurrencyStamp = Guid.NewGuid().ToString();
            IdentityResult result = await _roleManager.CreateAsync(role);
        }

        if (!await _roleManager.RoleExistsAsync("Convidado"))
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Convidado";
            role.NormalizedName = "CONVIDADO";
            role.ConcurrencyStamp = Guid.NewGuid().ToString();
            IdentityResult result = await _roleManager.CreateAsync(role);
        }
    }

    public async Task SeedUsersAsync()
    {
        if (await _userManager.FindByEmailAsync("felipemiranda0211@gmail.com") == null)
        {
            IdentityUser user = new IdentityUser();
            user.UserName = "FelipeAdm";
            user.NormalizedUserName = "FELIPEADM";
            user.Email = "felipemiranda0211@gmail.com";
            user.NormalizedEmail = "FELIPEMIRANDA0211@GMAIL.COM";
            user.EmailConfirmed = false;
            user.LockoutEnabled = false;
            user.SecurityStamp = Guid.NewGuid().ToString();
            IdentityResult result = await _userManager.CreateAsync(user, "Lipelipe#1");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Administrador");
            }
        }

        if (await _userManager.FindByEmailAsync("convidado@convidado.com") == null)
        {
            IdentityUser user = new IdentityUser();
            user.UserName = "Convidado";
            user.NormalizedUserName = "CONVIDADO";
            user.Email = "convidado@convidado.com";
            user.NormalizedEmail = "CONVIDADO@CONVIDADO.COM";
            user.EmailConfirmed = false;
            user.LockoutEnabled = false;
            user.SecurityStamp = Guid.NewGuid().ToString();
            IdentityResult result = await _userManager.CreateAsync(user, "Convidado#1");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Convidado");
            }
        }
    }
}